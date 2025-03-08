using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAttributes
{
    public class TangdaoAttribute : Attribute
    {
        public string Name { get; set; }

        public TangdaoAttribute()
        {
        }
    }
}