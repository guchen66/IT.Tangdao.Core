using IT.Tangdao.Core.Abstractions.FileAccessor;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;
using IT.Tangdao.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Infrastructure.Ambient;
using Newtonsoft.Json;

namespace IT.Tangdao.Core.Extensions
{
    public static class CacheContentExtensions
    {
        public static T Deserialize<T>(this ICacheContentQueryable cache, string path, DaoFileType type = DaoFileType.None) where T : class, new()
        {
            try
            {
                var rootKey = CacheKey.GetCacheKey(path, type);

                var parameter = TangdaoContext.GetTangdaoParameter(rootKey);

                string Data = parameter.Get<string>(rootKey);
                var detected = FileSelector.DetectFromContent(Data);
                T result = new T();

                switch (detected)
                {
                    case DaoFileType.Xml:
                        result = XmlFolderHelper.Deserialize<T>(Data);
                        break;

                    case DaoFileType.Json:
                        result = JsonConvert.DeserializeObject<T>(Data); ;
                        break;

                    case DaoFileType.Config:
                        result = ReadResult.Success(Data).ToObject<T>();
                        break;

                    default:
                        throw new NotSupportedException($"不支持的文件类型: {detected}");
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;// ReadResult<T>.FromException(ex);
            }
        }
    }
}