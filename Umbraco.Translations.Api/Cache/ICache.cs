namespace Umbraco.Translations.Api.Cache;

public interface ICache<TEntity>
{
    /// <summary>
    /// Fetch instance of <see cref="TEntity"/> through cache.
    /// </summary>
    /// <returns></returns>
    TEntity? FetchThroughCache(string cacheKey);
    
    /// <summary>
    /// Insert item in cache if not present.
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="cacheItem"></param>
    void AddToCache(string cacheKey, TEntity cacheItem);

    /// <summary>
    ///  Remove item from cache given cache key.
    /// </summary>
    /// <param name="cacheKey"></param>
    void RemoveFromCache(string cacheKey);
}