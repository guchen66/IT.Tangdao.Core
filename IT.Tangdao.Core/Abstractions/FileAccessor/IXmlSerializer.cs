using System;
using System.Xml.Linq;
using IT.Tangdao.Core.Enums;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IXmlSerializer
    {
        void ToXml<T>(T obj);
    }
}