namespace Umbraco.Translations.Api.Cache;

public class UmbracoCache<TEntity> : ICache<TEntity> where TEntity : class
{
    /// <inheritdoc />
    public async Task<TEntity?> FetchThroughCacheAsync()
    {
        throw new NotImplementedException();
    }
}