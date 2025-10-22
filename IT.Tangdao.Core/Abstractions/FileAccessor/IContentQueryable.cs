using System.Collections.Generic;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Selectors;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Helpers;
using IT.Tangdao.Core.Abstractions.FileAccessor;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    /// <summary>
    /// 链式查询起点。支持自动或显式指定格式后对内容做节点/实体查询。
    /// 所有方法线程安全且可重复调用（同一实例）。
    /// </summary>
    public interface IContentQueryable
    {
        public string Content { get; }

        IContentXmlQueryable AsXml();

        IContentJsonQueryable AsJson();

        IContentConfigQueryable AsConfig();

        IContentQueryable Read(string path, DaoFileType t = DaoFileType.None);

        //Task<IContentQueryable> ReadAsync(string path, DaoFileType daoFileType = DaoFileType.None);

        IContentQueryable Auto();          // 自动探测

        IContentQueryable this[int index] { get; }

        IContentQueryable this[string readObject] { get; }
    }
}