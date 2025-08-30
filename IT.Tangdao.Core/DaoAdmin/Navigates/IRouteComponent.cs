using IT.Tangdao.Core.DaoIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Navigates
{
    public interface IRouteComponent
    {
        ITangdaoPage ResolvePage(string route);
    }
}