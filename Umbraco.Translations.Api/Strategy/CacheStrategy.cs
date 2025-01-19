using Microsoft.Extensions.Logging;
using Umbraco.Translations.Api.Cache;

namespace Umbraco.Translations.Api.Strategy;

internal class CacheStrategy<TEntity> : ICacheStrategy<TEntity> where TEntity : class
{
    private ICache<TEntity> _cacheImplementation;
    private readonly ILogger<CacheStrategy<TEntity>> _logger;

    public CacheStrategy(ILogger<CacheStrategy<TEntity>> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public void SetCacheStrategy(ICache<TEntity> cacheStrategy)
    {
       _cacheImplementation = cacheStrategy;
    }
    
    /// <inheritdoc />
    public async Task<TEntity> ExecuteCacheStrategy(string[] cacheKeyPart, Func<TEntity> fallbackFunc)
    {
        try
        {
            // TODO: create builder for cacheKey
            var cacheKey = string.Join(":", cacheKeyPart);
            var entity = await  _cacheImplementation.FetchThroughCacheAsync(cacheKey) ?? fallbackFunc();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occured fetching cached entity with configured strategy {_cacheImplementation.GetType().Name}");
            throw;
        }
    }
}