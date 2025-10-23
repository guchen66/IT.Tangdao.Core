using IT.Tangdao.Core.Abstractions.Configurations;
using IT.Tangdao.Core.Abstractions.FileAccessor;
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
        public void Load(ITangdaoContainer container, DaoComponentContext context)
        {
            // 框架级默认服务
            container.AddTangdaoSingleton<IReadService, ReadService>();
            container.AddTangdaoSingleton<IWriteService, WriteService>();
            // container.AddTangdaoSingleton<IAlarmService, AlarmService>();
            // container.AddTangdaoSingleton<IMonitorService, FileMonitorService>();
            container.AddTangdaoSingleton<IDaoEventAggregator, DaoEventAggregator>();
            var loader = new TangdaoConfigLoader();
            // 2. 立即 Load 并塞进容器
            container.AddTangdaoSingleton(loader.Load());

            // 2. 默认通知器（用户可再注册覆盖）
            // container.AddTangdaoTransient<IAlarmNotifier, AlarmPopupNotifier>();
        }
    }
}