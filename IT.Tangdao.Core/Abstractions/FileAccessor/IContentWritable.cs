using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    public interface IContentWritable
    {
        /// <summary>
        /// 写入内容
        /// </summary>
        void Write(string path, string content, DaoFileType daoFileType = DaoFileType.None);

        /// <summary>
        /// 异步写入内容
        /// </summary>
        Task<WriteResult> WriteAsync(string path, string content, DaoFileType daoFileType = DaoFileType.None);

        /// <summary>
        /// 序列化对象并写入
        /// </summary>
        //void WriteObject<T>(string path, T obj);

        IContentWritable this[object writeObject] { get; }
    }
}