using IT.Tangdao.Core.Abstractions.Configurations;
using IT.Tangdao.Core.Abstractions.FileAccessor;
using IT.Tangdao.Core.Abstractions.Navigation;
using IT.Tangdao.Core.Abstractions.Notices;
using IT.Tangdao.Core.Commands;
using IT.Tangdao.Core.Components;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Bootstrap
{
    internal sealed class FrameworkDefaultComponent : ITangdaoContainerComponent
    {
        public void Load(ITangdaoContainer container, TangdaoComponentContext context)
        {
            //注册读写服务
            container.AddTangdaoSingleton<IContentAccess, ContentAccess>();

            //注册读取地址服务
            container.AddTangdaoSingleton<IFileLocator, FileLocator>();

            //注册发布通知服务
            container.AddTangdaoSingleton<ITangdaoPublisher, TangdaoPublisher>();
            container.AddTangdaoSingleton<ITangdaoNotifier, TangdaoNotifier>();

            //注册委托传输服务
            container.AddTangdaoSingleton<IActionTable, ActionTable>();

            //注册事件聚合器
            container.AddTangdaoSingleton<IDaoEventAggregator, DaoEventAggregator>();

            //注册导航服务

            container.AddTangdaoTransientFactory<ITangdaoRouterResolver>(provider =>
            {
                return new TangdaoRouterResolver(entry => provider.GetService(entry.RegisterType) as ITangdaoPage);
            });

            container.AddTangdaoSingleton<ITangdaoRouter, TangdaoRouter>();
            var loader = new TangdaoConfigLoader();
            // 2. 立即 Load 并塞进容器
            container.AddTangdaoSingleton(loader.Load());
        }
    }
}