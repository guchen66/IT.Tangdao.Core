using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    internal sealed class ContentWritable : IContentWritable
    {
        public object WriteObject
        {
            get => _writeObject;
            set => _writeObject = value;
        }

        private object _writeObject;

        // 实现接口中的索引器
        public IContentWritable this[object writeObject]
        {
            get
            {
                _writeObject = writeObject;
                return this;
            }
        }

        /// <summary>
        /// 写入内容
        /// </summary>
        public void Write(string path, string content, DaoFileType daoFileType = DaoFileType.None)
        {
            if (daoFileType == DaoFileType.None)
            {
                daoFileType = DaoFileType.Txt;
            }
            path.UseFileWriteToTxt(content);
        }

        /// <summary>
        /// 异步写入内容
        /// </summary>
        public async Task<WriteResult> WriteAsync(string path, string content, DaoFileType daoFileType = DaoFileType.None)
        {
            if (daoFileType == DaoFileType.None)
            {
                daoFileType = DaoFileType.Txt;
            }
            await new TimeSpan(1000);
            path.UseFileWriteToTxt(content);
            return WriteResult<string>.Success(content);
        }

        /// <summary>
        /// 序列化对象并写入
        /// </summary>
        //public void WriteObject<T>(string path, T obj)
        //{
        //    if (string.IsNullOrWhiteSpace(path))
        //        throw new ArgumentException("路径不能为空", nameof(path));

        //    if (obj == null)
        //        throw new ArgumentNullException(nameof(obj), "要序列化的对象不能为null");

        //    try
        //    {
        //        // 根据文件扩展名决定序列化格式
        //        var extension = Path.GetExtension(path)?.ToLowerInvariant();
        //        string content;

        //        switch (extension)
        //        {
        //            case ".xml":
        //                content = XmlSerializerHelper.Serialize(obj);
        //                break;
        //            case ".json":
        //            default:
        //                content = JsonConvert.SerializeObject(obj, _jsonSettings);
        //                break;
        //        }

        //        Write(path, content);

        //        LogHelper.Debug($"对象已序列化并写入: {path}, 类型: {typeof(T).Name}");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error($"序列化并写入对象失败。路径: {path}, 类型: {typeof(T).Name}", ex);
        //        throw new IOException($"序列化并写入对象失败: {path}", ex);
        //    }
        //}
    }
}