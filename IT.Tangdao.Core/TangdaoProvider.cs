using IT.Tangdao.Core.DaoCommon;
using IT.Tangdao.Core.DaoDtos.Globals;
using IT.Tangdao.Core.DaoEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core
{
    public sealed class TangdaoProvider : ITangdaoProvider
    {
        public object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public object Resolve(Type type, params object[] impleType)
        {
            return Activator.CreateInstance(type, impleType);
        }
    }
}