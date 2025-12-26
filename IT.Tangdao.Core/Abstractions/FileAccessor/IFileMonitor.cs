using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.EventArg;
using System;
using IT.Tangdao.Core.Configurations;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    /// <summary>
    /// 本地文件（增删改）监控接口
    /// </summary>
    public interface IFileMonitor : IDisposable
    {
        event EventHandler<DaoFileChangedEventArgs> FileChanged;

        /// <summary>
        /// 开始监控（使用默认配置）
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// 开始监控（使用自定义配置）
        /// </summary>
        void StartMonitoring(FileMonitorConfig config);

        /// <summary>
        /// 停止监控
        /// </summary>
        void StopMonitoring();

        /// <summary>
        /// 获取当前监控状态
        /// </summary>
        MonitorStatus GetStatus();
    }
}