using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Parameters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Sockets
{
    public interface ITangdaoSocketFactory
    {
        ITangdaoSocket CreateSocket(NetMode mode, ITangdaoUri uri);
    }
}