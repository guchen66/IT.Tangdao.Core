using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using IT.Tangdao.Core.Abstractions.Loggers;
using IT.Tangdao.Core.Paths;
using System.Xml;
using IT.Tangdao.Core.Configurations;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IXmlQueryable
    {
        ResponseResult SelectNode(string node);

        ResponseResult<IEnumerable<dynamic>> SelectNodes();

        ResponseResult<IEnumerable<T>> SelectNodes<T>() where T : new();
    }
}