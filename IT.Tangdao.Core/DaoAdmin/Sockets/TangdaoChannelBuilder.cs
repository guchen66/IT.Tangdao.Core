using IT.Tangdao.Core.DaoDtos;
using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.DaoParameters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Sockets
{
    public static class TangdaoChannelBuilder
    {
        public static TangdaoChannelContext Build(NetMode mode, string connStr)
        {
            var uri = new TangdaoUri(connStr);
            var channel = new TangdaoChannel(mode, uri);
            var request = new TangdaoRequest(channel);
            var response = new TangdaoResponse(channel);
            return new TangdaoChannelContext(channel, request, response);
        }
    }
}