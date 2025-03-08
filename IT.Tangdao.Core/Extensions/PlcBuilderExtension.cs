using IT.Tangdao.Core.DaoAdmin;
using IT.Tangdao.Core.DaoDtos.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class PlcBuilderExtension
    {
        public static IPlcBuilder RegisterPlcOption(this IPlcBuilder builder,Action<PlcOption> option) 
        {
            builder.Container.Configure(option);
            return builder;
        }
    }
}
