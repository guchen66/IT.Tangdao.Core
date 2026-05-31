using IT.Tangdao.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Infrastructure
{
    public static class DateTimeConverter
    {
        public static DateTime DefaultDateTime()
        {
            return DateTimeUtils.DateTime1970;
        }
    }
}