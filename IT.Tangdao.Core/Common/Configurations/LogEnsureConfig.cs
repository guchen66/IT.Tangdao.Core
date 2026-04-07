using IT.Tangdao.Core.Configurations;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Configurations
{
    /// <summary>
    /// 日志配置确保类
    /// </summary>
    /// <summary>
    /// 日志配置确保类
    /// </summary>
    public static class LogEnsureConfig
    {
        /// <summary>
        /// 加载日志配置
        /// </summary>
        /// <param name="logEntry">日志配置项</param>
        public static void Load(LogEntry logEntry)
        {
            if (logEntry == null)
                throw new ArgumentNullException(nameof(logEntry));

            TangdaoLoggerExtension.configs.Add(logEntry);
        }

        /// <summary>
        /// 加载多个日志配置
        /// </summary>
        /// <param name="logEntries">日志配置项列表</param>
        public static void Load(List<LogEntry> logEntries)
        {
            if (logEntries == null)
                throw new ArgumentNullException(nameof(logEntries));

            TangdaoLoggerExtension.configs.AddRange(logEntries);
        }
    }
}