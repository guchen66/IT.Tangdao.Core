using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface IJsonSerializer
    {
        void ToJson<T>(T obj);
    }
}