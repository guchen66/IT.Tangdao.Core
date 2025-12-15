using IT.Tangdao.Core.DaoTasks;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Events.Handlers
{
    public sealed class TangdaoWeakEvent<TArgs> where TArgs : TangdaoEventArgs
    {
        private static readonly TangdaoWeakEvent<TArgs> _instance = new TangdaoWeakEvent<TArgs>();
        public static TangdaoWeakEvent<TArgs> Instance => _instance;

        public event EventHandler<TArgs> MessageReceived;

        public event EventHandler<TArgs> OnMessageReceived
        {
            add => WeakEventManager<TangdaoWeakEvent<TArgs>, TArgs>.AddHandler(this, nameof(MessageReceived), value);
            remove => WeakEventManager<TangdaoWeakEvent<TArgs>, TArgs>.RemoveHandler(this, nameof(MessageReceived), value);
        }

        public void Publish(TArgs args)
        {
            TangdaoTaskScheduler.Execute(_ => MessageReceived?.Invoke(this, args), TaskThreadType.UI);
        }
    }
}