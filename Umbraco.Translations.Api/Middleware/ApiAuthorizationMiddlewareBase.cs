using Microsoft.AspNetCore.Http;

namespace Umbraco.Translations.Api.Middleware;

public abstract class ApiAuthorizationMiddlewareBase
{
    protected abstract string ApiKeyHeaderName { get; }
    protected abstract string ApiKeyValue { get; }
    protected abstract string ApiValidationPath { get; }
    
    private readonly RequestDelegate _next;

    protected ApiAuthorizationMiddlewareBase(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // If it doesn't start with the configure path, continue;
        if (!context.Request.Path.StartsWithSegments(ApiValidationPath))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedKey) || !providedKey.Equals(ApiKeyValue))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("UnAuthorized");
            return;
        }
        
        await _next(context);
    }
}