using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Components;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Ioc
{
    internal sealed class FrameworkDefaultComponent : ITangdaoContainerComponent
    {
        public void Load(ITangdaoContainer container, DaoComponentContext context)
        {
            // 框架级默认服务
            container.AddTangdaoSingleton<IReadService, ReadService>();
            container.AddTangdaoSingleton<IWriteService, WriteService>();
            container.AddTangdaoSingleton<IDaoEventAggregator, DaoEventAggregator>();
            // 后续继续加
        }
    }

    // FrameworkDefaultComponentModule.cs
    internal sealed class FrameworkDefaultComponentModule : ITangdaoModule
    {
        public void OnInitialized(ITangdaoProvider provider)
        {
        }

        public void RegisterServices(ITangdaoContainer container)
        {
            // 用内部 Component 机制完成注册
            container.RegisterComponent<FrameworkDefaultComponent>();
        }
    }
}