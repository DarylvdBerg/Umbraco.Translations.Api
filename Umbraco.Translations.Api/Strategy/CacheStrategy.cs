using Umbraco.Translations.Api.Cache;

namespace Umbraco.Translations.Api.Strategy;

internal class CacheStrategy<TEntity> : ICacheStrategy<TEntity> where TEntity : class
{
    private ICache<TEntity> _cacheImplementation;
    
    /// <inheritdoc />
    public void SetCacheStrategy(ICache<TEntity> cacheStrategy)
    {
       _cacheImplementation = cacheStrategy;
    }
    
    /// <inheritdoc />
    public async Task<TEntity> ExecuteCacheStrategy(string[] cacheKeyPart, Func<TEntity> fallbackFunc)
    {
        // TODO: create builder for cacheKey
        var cacheKey = string.Join(":", cacheKeyPart);
        var entity = await  _cacheImplementation.FetchThroughCacheAsync(cacheKey) ?? fallbackFunc();
        return entity;
    }
}