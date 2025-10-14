using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Providers
{
    /// <summary>
    /// 基于 TangdaoSortedDictionary 的通用优先级排序器
    /// </summary>
    public sealed class PrioritySortProvider<T> : IComparer<T>
    {
        private readonly Func<T, string> _keySelector;
        private readonly IDictionary<string, int> _priority;

        /// <summary>
        /// 优先级映射（值越小越靠前）
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="priority"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PrioritySortProvider(Func<T, string> keySelector,
                                    IDictionary<string, int> priority)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            _priority = priority ?? throw new ArgumentNullException(nameof(priority));
        }

        public int Compare(T x, T y)
        {
            var sx = _keySelector(x);
            var sy = _keySelector(y);

            // 找不到的分类排在最后
            var px = _priority.TryGetValue(sx, out var vx) ? vx : int.MaxValue;
            var py = _priority.TryGetValue(sy, out var vy) ? vy : int.MaxValue;

            int result = px.CompareTo(py);
            // 同优先级时按字符串本身排，保证稳定
            return result != 0 ? result : string.Compare(sx, sy, StringComparison.Ordinal);
        }
    }
}