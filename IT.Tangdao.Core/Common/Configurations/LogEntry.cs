using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Configurations
{
    /// <summary>
    /// 日志配置项
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// 日志保存路径
        /// </summary>
        public string SaveDir { get; set; }

        /// <summary>
        /// 日志间隔
        /// </summary>
        public TangdaoLogInterval LogInterval { get; set; }

        /// <summary>
        /// 日志格式
        /// </summary>
        public LogFormat LogFormat { get; set; }

        /// <summary>
        /// 开启日期目录
        /// </summary>
        public bool UseDatePath { get; set; }
    }
}