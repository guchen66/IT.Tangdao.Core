using IT.Tangdao.Core.DaoAdmin.Results;
using IT.Tangdao.Core.DaoConverters;
using IT.Tangdao.Core.Utilys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class ReadResultExtensions
    {
        public static Dictionary<string, string> ToDictionary(this ReadResult result)
        {
            if (!result.IsSuccess)
                return new Dictionary<string, string>(StringComparer.Ordinal);
            var dicts = result.ToReadResult<Dictionary<string, string>>();
            return dicts.Data
                         .OrderBy(kv => kv.Key)
                         .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.Ordinal);
        }

        /// <summary>
        /// 从泛型字典结果→POCO
        /// </summary>
        public static T ToObject<T>(this ReadResult result) where T : new()
        {
            var dict = result.ToDictionary();

            return DictToObject.Convert<T>(dict);
        }
    }
}