using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.Results;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IContentConfigQueryable : IContentQueryable
    {
        ReadResult SelectConfig(string section);

        ReadResult SelectConfig<T>(string section) where T : class, new();

        ReadResult SelectConfigByJsonConvert<T>(string section) where T : class, new();

        ReadResult SelectCustomConfig(string configName, string section);
    }
}