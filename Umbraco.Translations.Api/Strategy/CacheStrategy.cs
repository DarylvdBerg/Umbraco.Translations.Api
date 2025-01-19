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
    public async Task<TEntity> ExecuteCacheStrategy(Func<TEntity> fallbackFunc)
    {
        var entity = await  _cacheImplementation.FetchThroughCacheAsync() ?? fallbackFunc();
        return entity;
    }
}