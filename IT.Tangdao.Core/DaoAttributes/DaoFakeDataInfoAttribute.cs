using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DaoFakeDataInfoAttribute : Attribute
    {
        public string Message { get; }
        public Type EnumType { get; set; } // 用于指定枚举类型

        public DaoFakeDataInfoAttribute(string message)
        {
            Message = message;
        }
    }
}