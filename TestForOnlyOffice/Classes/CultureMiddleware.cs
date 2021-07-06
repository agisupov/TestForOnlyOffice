using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Classes
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            CultureInfo culture = null;

            if (context.User.Identity.IsAuthenticated)
            {
                var cultureCookie = context.User.FindFirstValue(ClaimTypes.Locality);

                culture = cultureCookie != null
                    ? CultureInfo.GetCultureInfo(cultureCookie)
                    : CultureInfo.CurrentCulture;
            }

            if (culture == null)
            {
                culture = new CultureInfo("en");
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await _next.Invoke(context);
        }
    }

    public static class CultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}
