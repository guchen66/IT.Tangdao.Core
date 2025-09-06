using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.DaoParameters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Sockets
{
    public interface ITangdaoSocket
    {
        NetMode Mode { get; }
        NetConnectionType ConnectionType { get; }
        bool IsConnected { get; }
        ITangdaoUri Uri { get; }

        Task<bool> ConnectAsync();

        Task DisconnectAsync();

        Task SendAsync(string message);

        Task<string> ReceiveAsync();

        event EventHandler<string> MessageReceived;

        event EventHandler<Exception> ErrorOccurred;
    }
}