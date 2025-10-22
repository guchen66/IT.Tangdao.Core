using IT.Tangdao.Core.Abstractions.Results;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IContentXmlQueryable : IContentQueryable
    {
        ReadResult SelectNode(string node);

        ReadResult SelectNodes(string uriPath);

        ReadResult<List<T>> SelectNodes<T>(string rootElement, Func<XElement, T> selector);

        ReadResult<List<T>> SelectNodes<T>() where T : new();
    }
}