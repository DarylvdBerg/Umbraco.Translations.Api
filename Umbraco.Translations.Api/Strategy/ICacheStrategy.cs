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
    Task<TEntity> ExecuteCacheStrategy(Func<TEntity> fallbackFunc);
}