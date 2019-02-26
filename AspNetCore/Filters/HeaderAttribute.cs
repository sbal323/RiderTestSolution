using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (! string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value)) 
                filterContext.HttpContext.Response.Headers.Add( Name, Value);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
//            context.HttpContext.Response.StatusCode = 500;
//            context.Result = new JsonResult(new {Error="Filter error"});
            base.OnActionExecuting(context);
        }
    }

}