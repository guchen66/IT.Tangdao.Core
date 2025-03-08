using IT.Tangdao.Core.DaoDtos.Options;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoIoc
{
    public class PlcIocService
    {
        public static void RegisterPlcServer(PlcOption Core)
        {
            PlcServerCollectionExtension.PlcOptions.Add(Core);
        }

       /* public static void RegisterPlcServer(List<IocConfig> iocList)
        {
            SugarServiceCollectionExtensions.configs.AddRange(iocList);
        }

        public static void ConfigurationPlc(Action<SqlSugarClient> action)
        {
            SugarServiceCollectionExtensions.Configuration = action;
        }*/
    }
}
