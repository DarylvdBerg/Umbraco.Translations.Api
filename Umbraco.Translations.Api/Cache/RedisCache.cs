namespace Umbraco.Translations.Api.Cache;

internal class RedisCache<TEntity> : ICache<TEntity>
{
    /// <inheritdoc />
    public async Task<TEntity> FetchThroughCacheAsync()
    {
        throw new NotImplementedException();
    }
}