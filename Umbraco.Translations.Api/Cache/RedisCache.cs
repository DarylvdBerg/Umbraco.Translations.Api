using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Umbraco.Translations.Api.Cache;

internal class RedisCache<TEntity> : ICache<TEntity>
{
    private readonly IDatabase _redisDb;

    public RedisCache(IDatabase redisDb)
    {
        _redisDb = redisDb;
    }
    
    /// <inheritdoc />
    public TEntity? FetchThroughCache(string cacheKey)
    {
        var entity = _redisDb.StringGet(cacheKey);
        if (!entity.HasValue || entity.IsNullOrEmpty)
        {
            return default;
        }
        
        var converted = JsonConvert.DeserializeObject<TEntity>(entity, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        
        return converted;
    }

    /// <inheritdoc />
    public void AddToCache(string cacheKey, TEntity cacheItem)
    {
        var redisKey = new RedisKey(cacheKey);
        var serializedEntity = JsonConvert.SerializeObject(cacheItem);
        var redisValue = new RedisValue(serializedEntity);
        _redisDb.StringSet(redisKey, redisValue);
    }

    /// <inheritdoc />
    public void RemoveFromCache(string cacheKey)
    {
        _redisDb.KeyDelete(cacheKey);
    }
}