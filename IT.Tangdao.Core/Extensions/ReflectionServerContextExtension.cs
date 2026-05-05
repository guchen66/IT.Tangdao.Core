using IT.Tangdao.Core.Common.Reflection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class ReflectionServerContextExtension
    {
        internal static HashSet<Assembly> assemblies = new HashSet<Assembly>();

        internal static ConcurrentDictionary<string, Type> reflectionTypes = new ConcurrentDictionary<string, Type>();

        public static void Builder(this ReflectionServerContext reflectionServerContext)
        {
            foreach (var item in reflectionServerContext._types)
            {
                reflectionTypes.GetOrAdd(item.Name, item);
            }
        }
    }
}