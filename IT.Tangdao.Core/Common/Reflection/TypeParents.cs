using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Reflection
{
    public readonly struct TypeParents
    {
        public Type? BaseClass { get; init; }      // 基类（null=object）
        public IReadOnlyList<Type> Interfaces { get; init; }

        // 语法糖：解构
        public void Deconstruct(out Type? baseClass, out IReadOnlyList<Type> interfaces)
        {
            baseClass = BaseClass;
            interfaces = Interfaces;
        }
    }
}