using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Helpers;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IConfigQueryable
    {
        ResponseResult<TangdaoSortedDictionary<string, string>> SelectAppSection(string section);

        ResponseResult SelectAppSection<T>(string section) where T : class, new();

        ResponseResult SelectConfigByJsonConvert<T>(string section) where T : class, new();

        ResponseResult<Dictionary<string, string>> SelectSection(string section);
    }
}