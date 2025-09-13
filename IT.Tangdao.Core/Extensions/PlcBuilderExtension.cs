using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Configurations;
using System;

namespace IT.Tangdao.Core.Extensions
{
    public static class PlcBuilderExtension
    {
        public static IPlcBuilder RegisterPlcOption(this IPlcBuilder builder, Action<PlcOption> option)
        {
            builder.Container.Configure(option);
            return builder;
        }
    }
}