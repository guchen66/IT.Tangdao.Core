using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Bootstrap
{
    internal sealed class FrameworkDefaultComponentModule : ITangdaoModule
    {
        public void RegisterServices(ITangdaoContainer container)
        {
            // 用内部 Component 机制完成注册
            container.RegisterComponent<FrameworkDefaultComponent>();
        }

        public void OnInitialized(ITangdaoProvider provider)
        {
            // 3. 把默认通知器自动挂上事件流
            // var service = provider.GetService<IAlarmService>();
            // var notifier = provider.GetService<IAlarmNotifier>();
            // service.Subscribe(notifier);
        }
    }
}