using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Helpers;

using IT.Tangdao.Core.Helpers;

using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions
{
    public class WriteService : IWriteService
    {
        public IContentWritable Default => new ContentWritable();
    }
}