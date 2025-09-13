using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewToViewModelAttribute : Attribute
    {
        public string ViewName { get; }

        public ViewToViewModelAttribute(string viewName)
        {
            ViewName = viewName;
        }
    }
}