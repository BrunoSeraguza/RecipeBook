using System.Globalization;

namespace MyRecipeBook.API.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;                       
        }

        public async Task Invoke( HttpContext context)
        {
            var suportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var language = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureInfo = new CultureInfo("en");

            if(!string.IsNullOrWhiteSpace(language) && suportedLanguages.Any(l => l.Name.Equals(language)))           
               cultureInfo = new CultureInfo(language);
            
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
