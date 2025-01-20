using Newtonsoft.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Extensions;

namespace Umbraco.Translations.Api.Cache;

public class UmbracoCache<TEntity> : ICache<TEntity> where TEntity : class
{
    private readonly IAppPolicyCache _appPolicyCache;

    public UmbracoCache(AppCaches appCaches)
    {
        _appPolicyCache = appCaches.RuntimeCache;
    }
    
    /// <inheritdoc />
    public TEntity? FetchThroughCache(string cacheKey)
    {
        var cachedItem = _appPolicyCache.GetCacheItem<TEntity>(cacheKey);
        if (cachedItem is null)
        {
            return null;
        }
        
        return cachedItem;
    }
    
    /// <inheritdoc />
    public void AddToCache(string cacheKey, TEntity cacheItem)
    {
        _appPolicyCache.InsertCacheItem(cacheKey, () => cacheItem);
    }

    /// <inheritdoc />
    public void RemoveFromCache(string cacheKey)
    {
        _appPolicyCache.ClearByKey(cacheKey);
    }
}