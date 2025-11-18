using IT.Tangdao.Core.Abstractions.FileAccessor;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IT.Tangdao.Core.Paths;
using IT.Tangdao.Core.Ambient;

namespace IT.Tangdao.Core.Extensions
{
    public static class CacheContentExtensions
    {
        /// <summary>
        /// 从缓存反序列化（同步） - string 路径
        /// </summary>
        public static T DeserializeCache<T>(this ICacheContentQueryable cache, string path, DaoFileType type = DaoFileType.None) where T : class, new()
            => ResolveInternal<T>(cache, path, type);

        /// <summary>
        /// 从缓存反序列化（同步） - AbsolutePath 路径
        /// </summary>
        public static T DeserializeCache<T>(this ICacheContentQueryable cache, AbsolutePath path, DaoFileType type = DaoFileType.None) where T : class, new()
            => ResolveInternal<T>(cache, path.Value, type);

        #region 私有实现

        private static T ResolveInternal<T>(ICacheContentQueryable cache, string fullPath, DaoFileType type) where T : class, new()
        {
            try
            {
                var rootKey = FileContentCacheKey.Create(fullPath, type);
                var parameter = TangdaoContext.GetTangdaoParameter(rootKey);
                string content = parameter.Get<string>(rootKey);

                var detected = FileHelper.DetectFromContent(content);

                switch (detected)
                {
                    case DaoFileType.Xml:
                        return TangdaoXmlSerializer.Deserialize<T>(content);

                    case DaoFileType.Json:
                        return JsonConvert.DeserializeObject<T>(content);

                    case DaoFileType.Config:
                        return ConfigFolderHelper.DeserializeObject<T>(content);

                    default:
                        throw new NotSupportedException($"不支持的文件类型: {detected}");
                }
            }
            catch
            {
                return default(T);
            }
        }

        #endregion 私有实现
    }
}