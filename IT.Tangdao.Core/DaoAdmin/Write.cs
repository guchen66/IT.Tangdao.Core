using IT.Tangdao.Core.DaoAdmin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin
{
    public sealed class Write : IWrite
    {
        public object WriteObject
        {
            get => _writeObject;
            set => _writeObject = value;
        }

        private object _writeObject;

        // 实现接口中的索引器
        public IWrite this[object writeObject]
        {
            get
            {
                _writeObject = writeObject;
                return this;
            }
        }

        public WriteResult WriteObjectToXML<TTarget>()
        {
            return WriteResult.Failure("");
        }

        public WriteResult WriteObjectToJson()
        {
            return WriteResult.Failure("");
        }

        public void Save()
        {
        }
    }
}