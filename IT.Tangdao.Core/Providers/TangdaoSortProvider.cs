using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Providers
{
    public class TangdaoSortProvider
    {
        /// <summary>
        /// 用 TangdaoSortedDictionary 做底层映射，支持 Key 有序遍历
        /// </summary>
        public static IComparer<T> Priority<T>(Func<T, string> keySelector,
                                               IEnumerable<KeyValuePair<string, int>> rules)
        {
            var dict = new TangdaoSortedDictionary<string, int>();
            foreach (var r in rules) dict.Add(r.Key, r.Value);
            return new PrioritySortProvider<T>(keySelector, dict);
        }

        /// <summary>
        /// 极简委托排序（无字典，最快）
        /// </summary>
        public static IComparer<T> Delegate<T>(Func<T, T, int> compare)
        {
            return Comparer<T>.Create(new Comparison<T>(compare));
        }
    }
}