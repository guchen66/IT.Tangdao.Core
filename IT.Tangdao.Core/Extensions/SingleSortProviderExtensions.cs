using IT.Tangdao.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class SingleSortProviderExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Func<T, IComparable> keySelector)
        {
            var sortProvider = new SingleSortProvider<T>(keySelector);
            return source.OrderBy(x => x, sortProvider);
        }
    }
}