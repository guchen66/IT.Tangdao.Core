using IT.Tangdao.Core.Configurations;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.DaoEvents;
using IT.Tangdao.Core.Parameters.EventArg;
using System;

namespace IT.Tangdao.Core.Abstractions.IServices
{
    public interface IMonitorService
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
        DaoMonitorStatus GetStatus();
    }
}