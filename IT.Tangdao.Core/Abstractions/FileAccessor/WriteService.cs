namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public class WriteService : IWriteService
    {
        public IContentWritable Default => new ContentWritable();
    }
}