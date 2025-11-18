namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public class ContentWriter : IContentWriter
    {
        public IContentWritable Default => new ContentWritable();
    }
}