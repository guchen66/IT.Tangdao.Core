using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoParameters.Infrastructure
{
    public interface ITangdaoUri
    {
        Uri Uri { get; }
        NetConnectionType ConnectionType { get; }
        string Host { get; }
        int Port { get; }
        string ComPort { get; }
        int BaudRate { get; }
        bool IsValid { get; }
    }
}