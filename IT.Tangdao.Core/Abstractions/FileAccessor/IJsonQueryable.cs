using IT.Tangdao.Core.Abstractions.Results;
using System.Collections.Generic;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IJsonQueryable
    {
        ResponseResult SelectValue(string key);

        ResponseResult<IEnumerable<dynamic>> SelectKeys();

        ResponseResult<IEnumerable<dynamic>> SelectValues();

        ResponseResult<IEnumerable<T>> SelectObjects<T>() where T : new();
    }
}