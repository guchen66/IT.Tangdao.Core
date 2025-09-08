using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.DaoParameters.Infrastructure;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Sockets
{
    public sealed class TangdaoChannel : ITangdaoChannel
    {
        private readonly ITangdaoSocket _socket;
        private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public TangdaoChannel(NetMode mode, ITangdaoUri uri)
        {
            _socket = new TangdaoSocketFactory().CreateSocket(mode, uri);
        }

        public bool IsConnected => _socket.IsConnected;

        public async Task<bool> ConnectAsync(CancellationToken token = default)
        {
            bool ok = false;
            try
            {
                ok = await _socket.ConnectAsync();
            }
            catch
            {
                ok = false;
            }
            _tcs.TrySetResult(ok);          // 一次性通知所有等待者
            return ok;
        }

        public Task<bool> WaitConnectedAsync(CancellationToken token = default)
            => _tcs.Task.WaitAsync(token);  // .NET 6+ 自带 WaitAsync

        public Task DisconnectAsync() => _socket.DisconnectAsync();

        public void Dispose()
        {
            (_socket as IDisposable)?.Dispose();
            _tcs.TrySetCanceled();
        }

        internal ITangdaoSocket Socket => _socket;
    }
}