using IT.Tangdao.Core.Abstractions.Results;
using System.Collections.Generic;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IJsonQueryable
    {
        ResponseResult SelectValue(string key);

        ResponseResult<List<dynamic>> SelectKeys();

        ResponseResult<List<dynamic>> SelectValues();

        ResponseResult<List<T>> SelectObjects<T>() where T : new();
    }
}