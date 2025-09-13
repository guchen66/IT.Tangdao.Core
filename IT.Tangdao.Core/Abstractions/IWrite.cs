using IT.Tangdao.Core.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    public interface IWrite
    {
        WriteResult WriteObjectToXML<TTarget>();

        WriteResult WriteObjectToJson();

        void Save();

        IWrite this[object writeObject] { get; }
    }
}