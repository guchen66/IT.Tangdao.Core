using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Reflection
{
    /// <summary>
    /// 父类构建模板
    /// </summary>
    public class TypeMetadata
    {
        public Type? BaseClass { get; init; }
        public IReadOnlyList<Type> Interfaces { get; init; }

        /// <summary>
        /// 此类是否单例
        /// </summary>
        public bool IsSingleton { get; }

        /// <summary>
        /// 是否可继承
        /// </summary>
        public bool IsSealed { get; }

        public TypeMetadata(Type baseClass, IReadOnlyList<Type> interfaces)
        {
            BaseClass = baseClass;
            Interfaces = interfaces ?? Array.Empty<Type>();
        }

        // 语法糖：解构
        public void Deconstruct(out Type? baseClass, out IReadOnlyList<Type> interfaces)
        {
            baseClass = BaseClass;
            interfaces = Interfaces;
        }
    }
}