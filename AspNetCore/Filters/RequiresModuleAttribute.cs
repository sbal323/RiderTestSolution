using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Filters
{
    public class RequiresModuleAttribute : TypeFilterAttribute
    {
        public RequiresModuleAttribute(params string[] modules)
            : base(typeof(RequiresModuleAttributeImpl))
        {
            this.Arguments = new[] { modules };
        }

        private class RequiresModuleAttributeImpl : Attribute, IAsyncResourceFilter
        {
            private readonly IEnumerable<string> _modules;

            public RequiresModuleAttributeImpl(params string[] modules)
            {
                _modules = modules;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                ClaimsPrincipal principal = context.HttpContext.User;

                IEnumerable<string> userModules = principal.Claims.Where(c => c.Type == "Module")
                    .Select(c => c.Value);

                if (userModules.Any(m => _modules.Contains(m)))
                {
                    await next();
                }
                else
                {
                    context.Result = new RedirectResult("/Account/AccessDenied");
                }
            }
        }
    }
}