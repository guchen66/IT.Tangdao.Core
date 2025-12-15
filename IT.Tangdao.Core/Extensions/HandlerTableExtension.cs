using IT.Tangdao.Core.Commands;
using IT.Tangdao.Core.DaoTasks;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public static class HandlerTableExtension
    {
        public static void Execute(this IHandlerTable table, string key, TaskThreadType affinity = TaskThreadType.Auto)
        {
            TangdaoTaskScheduler.Execute(_ =>
            {
                table.GetHandler(key)?.Invoke();
            }, affinity);
        }

        public static void Execute(this IHandlerTable table, string key, HandlerResult handlerResult, TaskThreadType affinity = TaskThreadType.Auto)
        {
            TangdaoTaskScheduler.Execute(_ =>
            {
                table.GetResultHandler(key)?.Invoke(handlerResult);
            }, affinity);
        }
    }
}