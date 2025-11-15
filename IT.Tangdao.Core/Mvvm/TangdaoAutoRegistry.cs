using IT.Tangdao.Core.Abstractions.Loggers;
using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Mvvm
{
    /// <summary>
    /// 自动注册所有视图
    /// </summary>
    internal sealed class TangdaoAutoRegistry
    {
        private static readonly ITangdaoLogger Logger = TangdaoLogger.Get(typeof(TangdaoAutoRegistry));

        public static void Register(ITangdaoContainer tangdaoContainer)
        {
            var AttributeInfos = TangdaoAttributeSelector.GetAttributeInfos<AutoRegisterAttribute>();
            Array.Sort(AttributeInfos, (a, b) => a.Attribute.CompareTo(b.Attribute));

            Logger.WriteLocal($"标记AutoRegisterAttribute特性的个数{AttributeInfos.Length}");
            foreach (var info in AttributeInfos)
            {
                AutoRegisterAttribute registerAttribute = info.Attribute;
                Type type = info.Type;
                switch (registerAttribute.Mode)
                {
                    case RegisterMode.Transient:
                        tangdaoContainer.AddTangdaoTransient(type);
                        break;

                    case RegisterMode.Singleton:
                        tangdaoContainer.AddTangdaoSingleton(type);
                        break;

                    case RegisterMode.Scoped:
                        tangdaoContainer.AddTangdaoScoped(type);
                        break;

                    default:
                        tangdaoContainer.AddTangdaoSingleton(type);
                        break;
                }
            }
        }
    }
}