using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionSelectorAttribute : Attribute
    {
        public string Employee { get; set; }
    }

    public class PercentPermissionAttribute : Attribute
    {
    }
}