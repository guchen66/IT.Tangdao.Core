using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Events
{
    public interface IEventAggregator
    {
        void Publish<T>(T @event = null) where T : EventBase, new();

        void Subscribe<T>(DaoEventHandler<T> handler) where T : EventBase, new();

        void UnSubscribe<T>(DaoEventHandler<T> handler) where T : EventBase, new();
    }
}