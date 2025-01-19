namespace Umbraco.Translations.Api.Cache;

internal class RedisCache<TEntity> : ICache<TEntity>
{
    /// <inheritdoc />
    public async Task<TEntity?> FetchThroughCacheAsync(string cacheKey)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TEntity>?> FetchAllThroughCache(string cacheKey)
    {
        throw new NotImplementedException();
    }
}