using IT.Tangdao.Core.DaoCommon;
using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.DaoEvents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Extensions
{
    public static class TangdaoProviderExtension
    {
        public static TService Resolve<TService>(this ITangdaoProvider provider)
        {
            Type serviceType = typeof(TService);
            //如果此类具有无参构造器，可以直接解析注册
            if (serviceType.GetConstructor(Type.EmptyTypes) != null)
            {
                return (TService)provider.Resolve(serviceType);
            }

            //否则，解析时，跟据数据通道传递数据上下文
            RegisterContext context = ChannelEvent.GetContext<TService>();

            if (context == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: {typeof(TService)}");
            }

            // 检查是否正在解析，避免循环依赖
            if (context.IsResolving)
            {
                throw new InvalidOperationException($"Circular dependency detected while resolving type: {typeof(TService)}");
            }

            context.IsResolving = true;

            //默认设置为瞬态
            context.Lifecycle = Lifecycle.Transient;
            try
            {
                // 如果请求的类型是接口，则查找对应的实现类
                Type implementationType = typeof(TService);
                if (typeof(TService).IsInterface)
                {
                    if (!context.InterfaceToImplementationMapping.TryGetValue(typeof(TService), out implementationType))
                    {
                        throw new InvalidOperationException($"No implementation registered for interface: {typeof(TService)}");
                    }
                }

                // 解析构造函数参数
                object[] parameterValues = new object[context.ParameterInfos.Length];
                for (int i = 0; i < context.ParameterInfos.Length; i++)
                {
                    var parameter = context.ParameterInfos[i];
                    Type parameterType = parameter.ParameterType;

                    // 递归解析参数类型
                    parameterValues[i] = ResolveParameter(provider, parameterType);
                }

                // 创建实例
                return (TService)Activator.CreateInstance(implementationType, parameterValues);
            }
            finally
            {
                context.IsResolving = false; // 重置标记
            }
        }

        private static object ResolveParameter(ITangdaoProvider provider, Type parameterType)
        {
            // 如果是基本类型或字符串，返回默认值
            if (parameterType == typeof(int))
            {
                return 0;
            }
            else if (parameterType == typeof(string))
            {
                return "default";
            }
            else if (parameterType == typeof(bool))
            {
                return false;
            }
            else if (parameterType.IsClass || parameterType.IsInterface)
            {
                // 如果参数类型是接口，则从映射中查找对应的实现类
                if (parameterType.IsInterface)
                {
                    var context = ChannelEvent.GetContext(parameterType);
                    object[] parameterValues = new object[context.ParameterInfos.Length];
                    if (context == null || !context.InterfaceToImplementationMapping.TryGetValue(parameterType, out var implementationType))
                    {
                        throw new InvalidOperationException($"No implementation registered for interface: {parameterType}");
                    }

                    // 递归解析实现类
                    // return provider.Resolve(implementationType);
                    // 解析构造函数参数

                    for (int i = 0; i < context.ParameterInfos.Length; i++)
                    {
                        var parameter = context.ParameterInfos[i];
                        parameterType = parameter.ParameterType;

                        // 递归解析参数类型
                        parameterValues[i] = ResolveParameter(provider, parameterType);
                    }

                    // 创建实例
                    return Activator.CreateInstance(implementationType, parameterValues);
                }

                // 如果是类，则直接递归解析
                return provider.Resolve(parameterType);
            }
            else if (parameterType.IsValueType)
            {
                // 创建值类型的默认实例
                return Activator.CreateInstance(parameterType);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported parameter type: {parameterType}");
            }
        }
    }
}