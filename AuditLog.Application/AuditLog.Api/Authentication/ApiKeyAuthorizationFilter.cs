using AuditLog.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AuditLog.Api.Authentication;

public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private readonly ApiKeyOptions _options;

    public ApiKeyAuthorizationFilter(IOptions<ApiKeyOptions> options)
    {
        _options = options.Value;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthorizationConstants.ApiKeyHeaderName, out var apiKeyValue))
        {
            context.Result = new UnauthorizedObjectResult("Api Key missing");
            return;
        }

        if (!apiKeyValue.Equals(_options.ApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid Api Key");
        }
    }
}