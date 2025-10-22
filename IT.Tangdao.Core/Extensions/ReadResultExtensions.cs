using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Helpers;
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
        public static TangdaoSortedDictionary<string, string> ToDictionary(this ReadResult result)
        {
            if (!result.IsSuccess)
                return new TangdaoSortedDictionary<string, string>(StringComparer.Ordinal);
            var dicts = result.ToReadResult<TangdaoSortedDictionary<string, string>>();
            return dicts.Data;
            // .OrderBy(kv => kv.Key)
            //  .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.Ordinal);
        }

        /// <summary>
        /// 从泛型字典结果→POCO
        /// </summary>
        public static T ToObject<T>(this ReadResult result) where T : new()
        {
            var dict = result.ToDictionary();

            return DictToObject.Convert<T>(dict);
        }

        public static List<string> ToList(this ReadResult result, string keyValue = null)
        {
            var dict = result.ToDictionary();

            return keyValue switch
            {
                null => dict.Values.ToList(),
                _ when string.Equals(keyValue, "value", StringComparison.OrdinalIgnoreCase)
                    => dict.Values.ToList(),
                _ when string.Equals(keyValue, "key", StringComparison.OrdinalIgnoreCase)
                    => dict.Keys.ToList(),
                _ => dict.Values.ToList() // 或抛出异常
            };
        }
    }
}