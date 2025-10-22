using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.FileAccessor;

namespace IT.Tangdao.Core.Abstractions
{
    /// <summary>
    /// 定义读取文本的服务
    /// </summary>
    public interface IReadService
    {
        /// <summary>
        /// 默认读取内容接口
        /// </summary>
        IContentQueryable Default { get; }

        /// <summary>
        /// 缓存读取数据接口
        /// </summary>
        ICacheContentQueryable Cache { get; }
    }
}