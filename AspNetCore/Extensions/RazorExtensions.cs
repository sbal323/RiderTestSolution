using System;
using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspNetCore.Extensions
{
    public static class RazorExtensions
    {
        public static HelperResult  RenderSectionWithDefault(this Microsoft.AspNetCore.Mvc.Razor.RazorPage webPage, 
            string name, Func<dynamic, HelperResult> defaultContents) {
            if (webPage.IsSectionDefined(name)) {
                webPage.RenderSection(name);
                return null;
            }
            else
            {
                return defaultContents(null);
            }
        }
    }
}