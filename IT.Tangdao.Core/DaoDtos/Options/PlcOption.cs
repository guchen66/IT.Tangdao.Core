﻿using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoDtos.Options
{
    public class PlcOption
    {
        public PlcType PlcType { get; internal set; }

        public string PlcIpAddress { get; internal set; }

        public string Port { get; internal set; }

        public bool IsAutoConnection { get; set; }

        public List<PlcOption> SlaveConnectionOptions { get; set; }
    }
}