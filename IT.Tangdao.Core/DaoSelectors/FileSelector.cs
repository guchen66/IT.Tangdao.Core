using IT.Tangdao.Core.DaoAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoSelectors
{
    public class FileSelector
    {
        public static IRead Queryable()
        {
            return new Read();
        }
    }
}