using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Navigates
{
    public interface IRouteComponent
    {
        ITangdaoPage ResolvePage(string route);
    }
}