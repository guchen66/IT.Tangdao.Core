namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    /// <inheritdoc/>
    public class ContentAccess : IContentAccess
    {
        public IContentBuilder Default => new ContentBuilder();

        public ICacheQueryBuilder Cache => new CacheQueryBuilder();
    }
}