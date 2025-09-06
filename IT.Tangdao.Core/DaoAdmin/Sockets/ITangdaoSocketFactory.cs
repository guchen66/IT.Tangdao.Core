using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.DaoParameters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Sockets
{
    public interface ITangdaoSocketFactory
    {
        ITangdaoSocket CreateSocket(NetMode mode, ITangdaoUri uri);
    }
}