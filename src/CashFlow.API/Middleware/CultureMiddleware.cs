using System.Globalization;

namespace CashFlow.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var acceptLanguage = context.Request.Headers.AcceptLanguage.ToString();

        var cultureCode = acceptLanguage?
            .Split(',')
            .FirstOrDefault()?
            .Split(';')
            .FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (!string.IsNullOrWhiteSpace(cultureCode))
        {
            cultureInfo = new CultureInfo(cultureCode);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
