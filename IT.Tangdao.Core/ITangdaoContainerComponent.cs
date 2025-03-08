using IT.Tangdao.Core.DaoCommon;
using IT.Tangdao.Core.DaoComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core
{
    public interface ITangdaoContainerComponent : ITangdaoComponent
    {
        void Load(ITangdaoContainer container, DaoComponentContext context);
    }
}