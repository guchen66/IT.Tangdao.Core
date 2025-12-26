using IT.Tangdao.Core.Abstractions.Results;
using System;
using System.IO;
using System.Linq;
using IT.Tangdao.Core.Helpers;
using System.Threading.Tasks;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Extensions;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public sealed class ContentWritable : IContentWritable
    {
        public string Content { get; set; }

        public string WritePath { get; }

        public DaoFileType DetectedType { get; }
    }
}