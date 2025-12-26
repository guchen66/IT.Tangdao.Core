using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;
using IT.Tangdao.Core.Paths;
using IT.Tangdao.Core.Configurations;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IContentBuilder
    {
        IContentQueryable Read(string path, DaoFileType t = DaoFileType.None);

        IContentQueryable Read(AbsolutePath path, DaoFileType t = DaoFileType.None);

        IContentWritable Write(string path, string content, DaoFileType daoFileType = DaoFileType.None);

        IContentWritable Write(AbsolutePath path, string content, DaoFileType daoFileType = DaoFileType.None);

        /// <summary>
        /// 获取空的内容查询器，用于不需要读取文件的情况（如配置文件读取）
        /// </summary>
        IContentQueryable Empty();
    }
}