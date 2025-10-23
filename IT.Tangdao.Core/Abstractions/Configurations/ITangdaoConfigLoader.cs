using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Configurations
{
    internal interface ITangdaoConfigLoader
    {
        TangdaoConfig Load();
    }
}