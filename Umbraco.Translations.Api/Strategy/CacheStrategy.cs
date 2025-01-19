using Microsoft.Extensions.Logging;
using Umbraco.Translations.Api.Cache;
using Umbraco.Translations.Api.Services;

namespace Umbraco.Translations.Api.Strategy;

internal class CacheStrategy<TEntity> : ICacheStrategy<TEntity> where TEntity : class
{
    private ICache<TEntity> _cacheImplementation;
    private readonly ILogger<CacheStrategy<TEntity>> _logger;
    private readonly ICacheKeyBuilder _cacheKeyBuilder;

    public CacheStrategy(ILogger<CacheStrategy<TEntity>> logger, ICacheKeyBuilder cacheKeyBuilder)
    {
        _logger = logger;
        _cacheKeyBuilder = cacheKeyBuilder;
    }
    
    /// <inheritdoc />
    public void SetCacheStrategy(ICache<TEntity> cacheStrategy)
    {
       _cacheImplementation = cacheStrategy;
    }

    /// <inheritdoc />
    public TEntity? FetchSingleCachedItem(string[] cacheKeyParts, Func<TEntity> fallbackFunc)
    {
        try
        {
            var cacheKey = _cacheKeyBuilder.BuildCacheKey(cacheKeyParts);
            var entity =  _cacheImplementation.FetchThroughCache(cacheKey);
            if (entity == null)
            {
                _logger.LogDebug($"No entity found in cache: {cacheKey}, fetching from source and adding to cache.");
                entity = fallbackFunc();
                _cacheImplementation.AddToCache(cacheKey, entity);
            }
            
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occured fetching cached entity with configured strategy {_cacheImplementation.GetType().Name}");
            throw;
        }
    }

    /// <inheritdoc />
    public IList<TEntity>? FetchMultipleCachedItem(string cacheKeySearch, Func<TEntity> fallbackFunc)
    {
        throw new NotImplementedException();
    }
}