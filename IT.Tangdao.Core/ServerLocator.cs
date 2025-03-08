using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core
{
    public class ServerLocator
    {
        public static ITangdaoProvider Current { get; set; }

        public static ITangdaoProvider InitContainer(ITangdaoContainer container)
        {
            Current = container.Builder();
            return Current;
        }
    }
}