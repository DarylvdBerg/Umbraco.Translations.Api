namespace Umbraco.Translations.Api.Cache;

public interface ICache<TEntity>
{
    /// <summary>
    /// Fetch instance of <see cref="TEntity"/> through cache.
    /// </summary>
    /// <returns></returns>
    TEntity? FetchThroughCache(string cacheKey);

    /// <summary>
    /// Fetch List of <see cref="TEntity"/>
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    IList<TEntity>? FetchAllThroughCache(string cacheKey);
    
    /// <summary>
    /// Insert item in cache if not present.
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="cacheItem"></param>
    void AddToCache(string cacheKey, TEntity cacheItem);
}