using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Filters
{
    public class CultureAttribute: ActionFilterAttribute
    {
        public string Name { get; set; }

        public static string CookieName
        {
            get
            {
                return "_Culture";
            }
        } 
        public override void OnActionExecuting(ActionExecutingContext filterContext) { 
            var culture = Name; 
            if (string.IsNullOrEmpty( culture)) 
                culture = GetSavedCultureOrDefault(filterContext.HttpContext.Request); 
            // Set culture on current thread
            SetCultureOnThread( culture);
            // Proceed as usual
            base.OnActionExecuting( filterContext);
         } 
        private static string GetSavedCultureOrDefault(HttpRequest httpRequest) { 
            var culture = CultureInfo.CurrentCulture.Name; 
            var cookie = httpRequest.Cookies[CookieName] ?? culture;
            return culture; 
        }

        public static void SetCultureCookie(HttpResponse httpResponse, string culture)
        {
            httpResponse.Cookies.Append(CookieName, culture, new CookieOptions(){Expires = DateTime.Now.AddDays(2)});
        }
        private static void SetCultureOnThread(string language)
        {
            var cultureInfo = new CultureInfo( language); 
            CultureInfo.CurrentCulture = cultureInfo; 
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}