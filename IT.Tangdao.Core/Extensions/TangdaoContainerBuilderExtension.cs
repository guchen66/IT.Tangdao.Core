using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Tangdao.Core.Extensions
{
    public static class TangdaoContainerBuilderExtension
    {
        // 存储注册的类型和它们的生命周期
        private static readonly Dictionary<Type, (Func<object> factory, Lifecycle lifecycle)> _registrations = new();

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ITangdaoContainerBuilder Singleton(this ITangdaoContainerBuilder container)
        {
            var lastRegistration = _registrations.Last();
            var type = lastRegistration.Key;
            _registrations[type] = (lastRegistration.Value.factory, Lifecycle.Singleton);
            return container;
        }

        /// <summary>
        /// 瞬态模式
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ITangdaoContainerBuilder Transient(this ITangdaoContainerBuilder container)
        {
            ITangdaoAdapter tangdaoAdapter = (ITangdaoAdapter)container;

            return new TangdaoContainerBuilder();
        }

        /// <summary>
        /// 作用域模式
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ITangdaoContainerBuilder Scoped(this ITangdaoContainerBuilder container)
        {
            return new TangdaoContainerBuilder();
        }
    }
}