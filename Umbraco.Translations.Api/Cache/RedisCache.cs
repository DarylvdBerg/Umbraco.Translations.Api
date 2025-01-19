namespace Umbraco.Translations.Api.Cache;

internal class RedisCache<TEntity> : ICache<TEntity>
{
    /// <inheritdoc />
    public TEntity? FetchThroughCache(string cacheKey)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IList<TEntity>? FetchAllThroughCache(string cacheKey)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void AddToCache(string cacheKey, TEntity cacheItem)
    {
        throw new NotImplementedException();
    }
}