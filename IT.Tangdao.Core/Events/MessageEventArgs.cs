using IT.Tangdao.Core.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Events
{
    public class MessageEventArgs : TangdaoEventArgs
    {
        public MessageContext Context { get; set; }

        public bool Cancel { get; set; }

        public MessageEventArgs(MessageContext context)
        {
            Context = context;
        }
    }
}