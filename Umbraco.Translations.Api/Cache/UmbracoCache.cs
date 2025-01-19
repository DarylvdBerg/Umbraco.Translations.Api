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
    public async Task<TEntity?> FetchThroughCacheAsync(string cacheKey)
    {
        var cachedItem = _appPolicyCache.Get(cacheKey);
        if (cachedItem is null)
        {
            return null;
        }
        
        var convertResult = cachedItem.TryConvertTo<TEntity>();

        if (!convertResult.Success)
        {
            return null;
        }
        
        var entity = convertResult.Result;
        return entity;
    }

    public async Task<IList<TEntity>?> FetchAllThroughCache(string cacheKey)
    {
        var cachedItems = _appPolicyCache.GetCacheItemsByKeySearch<TEntity>(cacheKey).ToList();
        if (!cachedItems.Any())
        {
            return null;
        }
        
        return cachedItems;
    }
}