using IT.Tangdao.Core.Commands;
using IT.Tangdao.Core.DaoTasks;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.EventArg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Events
{
    public sealed class TangdaoWeakEvent
    {
        private static readonly TangdaoWeakEvent _instance = new TangdaoWeakEvent();
        public static TangdaoWeakEvent Instance => _instance;

        private event EventHandler<MessageEventArgs> MessageReceived;

        [Obsolete("仅供框架内部使用，请订阅 OnXxxReceived", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<KeyMessageEventArgs> KeyMessageReceived;

        [Obsolete("仅供框架内部使用，请订阅 OnXxxReceived", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<HandlerTableEventArgs> HandlerTableReceived;

        public event EventHandler<MessageEventArgs> OnMessageReceived
        {
            add => WeakEventManager<TangdaoWeakEvent, MessageEventArgs>.AddHandler(this, "MessageReceived", value);
            remove => WeakEventManager<TangdaoWeakEvent, MessageEventArgs>.RemoveHandler(this, nameof(MessageReceived), value);
        }

        public event EventHandler<KeyMessageEventArgs> OnKeyMessageReceived
        {
            add => WeakEventManager<TangdaoWeakEvent, KeyMessageEventArgs>.AddHandler(this, nameof(KeyMessageReceived), value);
            remove => WeakEventManager<TangdaoWeakEvent, KeyMessageEventArgs>.RemoveHandler(this, nameof(KeyMessageReceived), value);
        }

        public event EventHandler<HandlerTableEventArgs> OnHandlerTableReceived
        {
            add => WeakEventManager<TangdaoWeakEvent, HandlerTableEventArgs>.AddHandler(this, nameof(HandlerTableReceived), value);
            remove => WeakEventManager<TangdaoWeakEvent, HandlerTableEventArgs>.RemoveHandler(this, nameof(HandlerTableReceived), value);
        }

        public void Publish(MessageEventArgs message)
        {
            TangdaoTaskScheduler.Execute(dao =>
            {
                MessageReceived?.Invoke(this, message);
            }, TaskThreadType.UI);
        }

        public void Publish(string key, MessageEventArgs message)
        {
            var kargs = new KeyMessageEventArgs(key, message);
            TangdaoTaskScheduler.Execute(dao =>
            {
                KeyMessageReceived?.Invoke(this, kargs);
            }, TaskThreadType.UI);
        }

        public void Publish(string key, IHandlerTable handlerTable)
        {
            var args = new HandlerTableEventArgs(key, handlerTable);
            TangdaoTaskScheduler.Execute(dao =>
            {
                HandlerTableReceived?.Invoke(this, args);
            }, TaskThreadType.UI);
        }
    }
}