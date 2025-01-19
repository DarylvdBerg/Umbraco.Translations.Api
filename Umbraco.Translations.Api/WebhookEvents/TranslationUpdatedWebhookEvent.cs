using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Core.Webhooks;

namespace Umbraco.Translations.Api.NotificationHandlers;

public class TranslationUpdatedWebhookEvent : WebhookEventContentBase<ContentPublishedNotification, IContent>
{
    public TranslationUpdatedWebhookEvent(IWebhookFiringService webhookFiringService, IWebhookService webhookService, IOptionsMonitor<WebhookSettings> webhookSettings, IServerRoleAccessor serverRoleAccessor) : base(webhookFiringService, webhookService, webhookSettings, serverRoleAccessor)
    {
    }

    public override string Alias { get; }
    protected override IEnumerable<IContent> GetEntitiesFromNotification(ContentPublishedNotification notification)
    {
        throw new NotImplementedException();
    }

    protected override object? ConvertEntityToRequestPayload(IContent entity)
    {
        throw new NotImplementedException();
    }
}