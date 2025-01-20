using Umbraco.Translations.Api.Cache;

namespace Umbraco.Translations.Api.Strategy;

public interface ICacheStrategy<TEntity> where TEntity : class
{
    /// <summary>
    /// Set configured caching strategy
    /// </summary>
    /// <param name="cacheStrategy"></param>
    void SetCacheStrategy(ICache<TEntity> cacheStrategy);
    
    /// <summary>
    /// Execute cache strategy implementation
    /// </summary>
    /// <param name="fallbackFunc"></param>
    /// <returns></returns>
    TEntity? FetchSingleCachedItem(string[] cacheKeyParts, Func<TEntity> fallbackFunc);
    
    /// <summary>
    /// Strategy implementation for removing a item from the configured cache
    /// </summary>
    /// <param name="cacheKey"></param>
    void RemoveFromCache(string cacheKey);
    
}