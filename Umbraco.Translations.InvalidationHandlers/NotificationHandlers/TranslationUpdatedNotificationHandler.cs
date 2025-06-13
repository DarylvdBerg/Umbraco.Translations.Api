using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Translations.Api.Services;
using Umbraco.Translations.Cache.Strategy;

namespace Umbraco.Translations.InvalidationHandlers.NotificationHandlers;

internal abstract class TranslationUpdatedNotificationHandler(ICacheStrategy cacheStrategy, ICacheKeyBuilder cacheKeyBuilder)
    : INotificationHandler<DictionaryItemSavedNotification>
{
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
                var cacheKey = cacheKeyBuilder.BuildCacheKey([dictionaryKey, culture]);
                cacheStrategy.RemoveFromCache(cacheKey);
            }
        }
    }
}