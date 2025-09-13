using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.Components;
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