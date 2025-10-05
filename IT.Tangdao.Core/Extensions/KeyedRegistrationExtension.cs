using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class KeyedRegistrationExtension
    {
        #region 泛型便利

        public static ITangdaoContainer AddKeyedTransient<TService, TImpl>(this ITangdaoContainer container,
                                                                            object key)
            where TImpl : TService
            => AddKeyed<TService, TImpl>(container, key, new TransientStrategy());

        public static ITangdaoContainer AddKeyedSingleton<TService, TImpl>(this ITangdaoContainer container,
                                                                            object key)
            where TImpl : TService
            => AddKeyed<TService, TImpl>(container, key, new SingletonStrategy());

        public static ITangdaoContainer AddKeyedScoped<TService, TImpl>(this ITangdaoContainer container,
                                                                         object key)
            where TImpl : TService
            => AddKeyed<TService, TImpl>(container, key, new ScopedStrategy());

        #endregion 泛型便利

        #region 底层统一入口

        private static ITangdaoContainer AddKeyed<TService, TImpl>(ITangdaoContainer container,
                                                                   object key,
                                                                   ILifecycleStrategy strategy)
        {
            var entry = new KeyedServiceEntry(typeof(TService), typeof(TImpl), strategy, key);
            container.Register(entry);
            return container;
        }

        #endregion 底层统一入口
    }
}