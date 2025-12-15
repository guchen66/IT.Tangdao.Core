using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Commands
{
    public interface IHandlerTable
    {
        void Register(string key, Action action);

        void Register(string key, Action<HandlerResult> action);

        Action GetHandler(string key);

        Action<HandlerResult> GetResultHandler(string key);
    }
}