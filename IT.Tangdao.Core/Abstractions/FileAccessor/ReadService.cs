using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Selectors;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Abstractions.FileAccessor;

namespace IT.Tangdao.Core.Abstractions
{
    /// <inheritdoc/>
    public class ReadService : IReadService
    {
        public IContentQueryable Default => new ContentQueryable();

        public ICacheContentQueryable Cache => new CacheContentQueryable();
    }
}