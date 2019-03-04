using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        protected DateTime StartTime;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.ActionDescriptor.RouteValues["action"];
            if (string.Equals(action, "index", StringComparison.CurrentCultureIgnoreCase))
            {
                StartTime = DateTime.Now;
            } 
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var action = filterContext.ActionDescriptor.RouteValues["action"];
            if (string.Equals(action, "index", StringComparison.CurrentCultureIgnoreCase))
            {
                var timeSpan = DateTime.Now - StartTime; 
                filterContext.HttpContext.Response.Headers.Add("duration", timeSpan.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            }
            base.OnActionExecuted(filterContext);
        }
        [Header(Name="Action", Value="About")]
        public IActionResult Index(string page)
        {
            if (!string.IsNullOrWhiteSpace(page) && page.Equals("/Privacy"))
            {
                return Privacy();
            }
            var model = new AboutModel( "About") {Email = new EmailData(){To = "sbal323@gmail.com", Subject = "Sales request", Body = "Request the price list"}};

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                // log error.Error
            }
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier}); 
        }
    }
}