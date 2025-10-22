using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    /// <summary>
    /// 定义写入文本的服务
    /// </summary>
    public interface IWriteService
    {
        /// <summary>
        /// 默认写入内容接口
        /// </summary>
        IContentWritable Default { get; }
    }
}