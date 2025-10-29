using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IContentIniQueryable
    {
        ResponseResult<IniConfig> SelectIni(string section);
    }
}