using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    internal static class ServiceRegistryExtension
    {
        public static void ValidateDependencies(this IServiceRegistry registry)
        {
            var visitor = new DependencyValidationVisitor(registry);
            foreach (var entry in registry.GetAllEntries())
                visitor.Visit(entry);
        }
    }
}