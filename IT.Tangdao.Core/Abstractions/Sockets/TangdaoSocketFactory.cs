using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Sockets
{
    public class TangdaoSocketFactory : ITangdaoSocketFactory
    {
        public ITangdaoSocket CreateSocket(NetMode mode, ITangdaoUri uri)
        {
            return uri.ConnectionType switch
            {
                NetConnectionType.Tcp => new TcpTangdaoSocket(mode, uri),
                NetConnectionType.Udp => new UdpTangdaoSocket(mode, uri), // 需要实现UDP版本
                NetConnectionType.Serial => new SerialTangdaoSocket(mode, uri), // 需要实现串口版本
                _ => throw new NotSupportedException($"不支持的连接类型: {uri.ConnectionType}")
            };
        }
    }
}