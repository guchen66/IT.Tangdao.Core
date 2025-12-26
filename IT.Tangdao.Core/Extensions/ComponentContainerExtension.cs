using IT.Tangdao.Core.Bootstrap;
using IT.Tangdao.Core.Components;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    internal static class ComponentContainerExtension
    {
        public static ITangdaoContainer RegisterComponent<TComponent>(this ITangdaoContainer container, object options = default)
            where TComponent : class, ITangdaoContainerComponent, new()
        {
            return container.RegisterComponent<TComponent, object>(options);
        }

        public static ITangdaoContainer RegisterComponent<TComponent, TComponentOptions>(this ITangdaoContainer container, TComponentOptions options = default)
            where TComponent : class, ITangdaoContainerComponent, new()
        {
            return container.RegisterComponent(typeof(TComponent), options);
        }

        public static ITangdaoContainer RegisterComponent(this ITangdaoContainer container, Type componentType, object options = default)
        {
            // 创建组件依赖链
            var componentContextLinkList = ManualDependProvider.CreateDependLinkList(componentType, options);

            // 逐条创建组件实例并调用
            foreach (var context in componentContextLinkList)
            {
                // 创建组件实例
                var component = Activator.CreateInstance(context.ComponentType) as ITangdaoContainerComponent;
                // 调用
                component.Load(container, context);
            }

            return container;
        }
    }
}