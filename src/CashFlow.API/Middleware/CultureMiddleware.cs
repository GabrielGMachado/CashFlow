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
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureCode = requestedCulture?
            .Split(',')       
            .FirstOrDefault()? 
            .Split(';')       
            .FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (!string.IsNullOrWhiteSpace(cultureCode) 
            && supportedLanguages.Exists(language => language.Name.Equals(cultureCode)))
        {
            cultureInfo = new CultureInfo(cultureCode);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
