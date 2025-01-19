namespace Umbraco.Translations.Api.Cache;

public interface ICache<TEntity>
{
    /// <summary>
    /// Fetch instance of <see cref="TEntity"/> through cache.
    /// </summary>
    /// <returns></returns>
    Task<TEntity?> FetchThroughCacheAsync(string cacheKey);
}