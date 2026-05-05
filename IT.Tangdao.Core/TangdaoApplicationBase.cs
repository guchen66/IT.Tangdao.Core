using IT.Tangdao.Core.Bootstrap;
using IT.Tangdao.Core.DaoTasks;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core
{
    public abstract class TangdaoApplicationBase : Application, IAppHost<TangdaoHost>, ITangdaoDataProvider
    {
        public TangdaoPipe<TangdaoHost> Handler { get; set; }

        public IBindHandler Binding { get; } = new BindHandler();

        protected virtual void RegisterServices(ITangdaoContainer container)
        {
        }

        protected virtual TangdaoContainerBuilder CreateContainer()
        {
            return new TangdaoContainerBuilder();
        }

        public virtual async Task AsyncTaskHandler(ITaskQueueManager taskQueueManager)
        {
            await taskQueueManager.Empty();
        }
    }
}