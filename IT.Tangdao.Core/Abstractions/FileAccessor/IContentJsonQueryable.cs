using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.Results;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IContentJsonQueryable : IContentQueryable
    {
        ResponseResult SelectKeys();

        ResponseResult SelectValue(string key);
    }
}