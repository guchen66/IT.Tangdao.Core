using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Commands
{
    public class HandlerResult
    {
        public BackResult Result { get; set; }

        public string Name { get; set; }

        public ITangdaoParameter Parameter { get; set; }
    }
}