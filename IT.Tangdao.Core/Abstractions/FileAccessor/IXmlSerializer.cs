using System;
using System.Xml.Linq;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IXmlSerializer
    {
        void ToXml<T>(T obj);
    }
}