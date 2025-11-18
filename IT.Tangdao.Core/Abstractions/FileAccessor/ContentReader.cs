namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    /// <inheritdoc/>
    public class ContentReader : IContentReader
    {
        public IContentQueryable Default => new ContentQueryable();

        public ICacheContentQueryable Cache => new CacheContentQueryable();
    }
}