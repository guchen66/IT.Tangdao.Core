using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Sockets
{
    public static class TangdaoSocketMessage
    {
        private static ITangdaoSocket _socket;
        private static ITangdaoSocketFactory _factory = new TangdaoSocketFactory();

        public static event EventHandler<string> MessageReceived;

        public static event EventHandler<Exception> ErrorOccurred;

        public static void Init(NetMode netMode, string connectionString)
        {
            var uri = new TangdaoUri(connectionString);
            if (!uri.IsValid)
                throw new ArgumentException("无效的连接字符串");

            _socket = _factory.CreateSocket(netMode, uri);
            _socket.MessageReceived += (s, msg) => MessageReceived?.Invoke(s, msg);
            _socket.ErrorOccurred += (s, ex) => ErrorOccurred?.Invoke(s, ex);
        }

        public static void Init(NetMode netMode, ITangdaoUri uri)
        {
            _socket = _factory.CreateSocket(netMode, uri);
            _socket.MessageReceived += (s, msg) => MessageReceived?.Invoke(s, msg);
            _socket.ErrorOccurred += (s, ex) => ErrorOccurred?.Invoke(s, ex);
        }

        public static async Task<bool> ConnectAsync()
        {
            if (_socket == null)
                throw new InvalidOperationException("请先调用Init方法初始化");

            return await _socket.ConnectAsync();
        }

        public static async Task DisconnectAsync()
        {
            if (_socket != null)
                await _socket.DisconnectAsync();
        }

        public static async Task SendAsync(string message)
        {
            if (_socket?.IsConnected == true)
                await _socket.SendAsync(message);
        }

        public static bool IsConnected => _socket?.IsConnected == true;
        public static NetMode? CurrentMode => _socket?.Mode;
        public static NetConnectionType? CurrentConnectionType => _socket?.ConnectionType;
    }
}