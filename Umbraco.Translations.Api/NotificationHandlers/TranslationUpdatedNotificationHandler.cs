using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Translations.Api.Models;
using Umbraco.Translations.Api.Services;
using Umbraco.Translations.Api.Strategy;

namespace Umbraco.Translations.Api.NotificationHandlers;

public class TranslationUpdatedNotificationHandler: INotificationHandler<DictionaryItemSavedNotification>
{
    private readonly ICacheStrategy<ITranslation> _cacheStrategy;
    private readonly ICacheKeyBuilder _cacheKeyBuilder;

    public TranslationUpdatedNotificationHandler(ICacheStrategy<ITranslation> cacheStrategy, ICacheKeyBuilder cacheKeyBuilder)
    {
        _cacheStrategy = cacheStrategy;
        _cacheKeyBuilder = cacheKeyBuilder;
    }
    
    public void Handle(DictionaryItemSavedNotification notification)
    {
        // Get all saved entities
        var entities = notification.SavedEntities;
        
        // Loop over each entity to remove from cache.
        foreach (var entity in entities)
        {
            var dictionaryKey = entity.ItemKey;
            var cultures = entity.Translations.Select(trans => trans.LanguageIsoCode);
            
            // Loop over each configured culture to remove.
            foreach (var culture in cultures)
            {
                var cacheKey = _cacheKeyBuilder.BuildCacheKey([dictionaryKey, culture]);
                _cacheStrategy.RemoveFromCache(cacheKey);
            }
        }
    }
}