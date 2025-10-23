namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    /// <inheritdoc/>
    public class ReadService : IReadService
    {
        public IContentQueryable Default => new ContentQueryable();

        public ICacheContentQueryable Cache => new CacheContentQueryable();
    }
}