using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Configurations;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IIniQueryable
    {
        ResponseResult<IniConfig> SelectIni(string section);
    }
}