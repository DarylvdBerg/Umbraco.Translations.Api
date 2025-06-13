using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Translations.InvalidationHandlers.NotificationHandlers;

namespace Umbraco.Translations.InvalidationHandlers.Composer;

public class UmbracoTranslationApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<DictionaryItemSavedNotification, TranslationUpdatedNotificationHandler>();
    }
}