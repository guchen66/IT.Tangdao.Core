using IT.Tangdao.Core.Abstractions.Contracts;
using IT.Tangdao.Core.Abstractions.Loggers;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Ioc;
using IT.Tangdao.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Bootstrap
{
    internal sealed class AutoRegisterComponentModule : ITangdaoModule
    {
        private static readonly ITangdaoLogger Logger = TangdaoLogger.Get(typeof(AutoRegisterComponentModule));

        public void RegisterServices(ITangdaoContainer container)
        {
            TangdaoAutoRegistry.Register(container);
            Logger.WriteLocal("所有View注册成功");
        }

        public void OnInitialized(ITangdaoProvider provider)
        {
        }
    }
}