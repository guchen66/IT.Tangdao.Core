using IT.Tangdao.Core.DaoTasks;
using IT.Tangdao.Core.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Bootstrap
{
    /// <summary>
    /// 提供机器宿主可回调钩子
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAppHost<T>
    {
        /// <summary>
        /// 提供事件通道
        /// </summary>
        TangdaoPipe<T> Handler { get; }

        /// <summary>
        /// 创建宿主数据
        /// </summary>
        /// <returns></returns>
        T CreateHost();

        /// <summary>
        /// 处理自动绑定
        /// </summary>
        IBindHandler Binding { get; }

        /// <summary>
        /// 提供异步任务流
        /// </summary>
        /// <param name="taskQueueManager"></param>
        /// <returns></returns>
        Task AsyncTaskHandler(ITaskQueueManager taskQueueManager);
    }
}