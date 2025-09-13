using IT.Tangdao.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core
{
    public abstract class TangdaoAdapter
    {
        public readonly List<RegisterContext> CurrentContext = new List<RegisterContext>();

    }
}
