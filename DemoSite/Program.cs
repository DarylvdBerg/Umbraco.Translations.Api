using Umbraco.Translations.Api.Extensions;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddDeliveryApi()
            .AddComposers()
            .Build();
        
        builder.RegisterTranslationsApi();

        WebApplication app = builder.Build();

        await app.BootUmbracoAsync();
        app.ConfigureApiMiddleware();
        
        app.UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
                u.UseWebsite();
            })
            .WithEndpoints(u =>
            {
                u.UseInstallerEndpoints();
                u.UseBackOfficeEndpoints();
                u.UseWebsiteEndpoints();
            });

        await app.RunAsync();
    }
}



