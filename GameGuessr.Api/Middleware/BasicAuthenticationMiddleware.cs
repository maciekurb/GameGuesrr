using System.Net;
using System.Text;
using Hangfire.Dashboard;
using Microsoft.Extensions.Options;

namespace GameGuessr.Api.Middleware;

public class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sites = new List<string> { "hangfire" };

        if (!sites.Any(site => context.Request.Path.StartsWithSegments($"/{site}")))
        {
            await _next.Invoke(context);

            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
            var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

            var username = decodedUsernamePassword.Split(':', 2)[0];
            var password = decodedUsernamePassword.Split(':', 2)[1];

            if (IsAuthorized(username, password))
            {
                await _next.Invoke(context);

                return;
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }

    private bool IsAuthorized(string username, string password) =>
        username.Equals("dashboard", StringComparison.InvariantCultureIgnoreCase) && password.Equals("koszyk79");
}

public static class BasicAuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseBasicAuthenticationForRestrictedRoutes(this IApplicationBuilder builder) =>
        builder.UseMiddleware<BasicAuthenticationMiddleware>();
}

public class AllowAllFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}
