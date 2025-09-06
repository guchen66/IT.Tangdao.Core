using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Sockets
{
    public interface ITangdaoRequest
    {
        Task SendAsync(string message, CancellationToken token = default);
    }
}