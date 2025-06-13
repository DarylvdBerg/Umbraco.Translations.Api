using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Translations.Api.NotificationHandlers;

namespace Umbraco.Translations.Api.Composer;

public class UmbracoTranslationApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<DictionaryItemSavedNotification, TranslationUpdatedNotificationHandler>();
    }
}