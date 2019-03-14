using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
using AspNetCore.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.Controllers
{
    [HandleControllerException]
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
                filterContext.HttpContext.Response.Headers.Add("duration",
                    timeSpan.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            }

            base.OnActionExecuted(filterContext);
        }

        [Header(Name = "Action", Value = "About")]
        public async Task<IActionResult> Index(string page,[FromServices] ISharePointService service)
        {
            if (!string.IsNullOrWhiteSpace(page) && page.Equals("/Privacy"))
            {
                return await Privacy(service);
            }

            var model = new AboutModel("About")
            {
                Email = new EmailData()
                    {To = "sbal323@gmail.com", Subject = "Sales request", Body = "Request the price list"}
            };

            return View(model);
        }

        class ErrorMessage
        {
            public string Error { get; set; }
        }

        public async Task<IActionResult> Privacy([FromServices] ISharePointService service)
        {
            var t1 = Thread.CurrentThread.ManagedThreadId.ToString();
            var spText = await service.GetData<ErrorMessage>();
            var t2 = Thread.CurrentThread.ManagedThreadId.ToString();
            return View(new BaseModel($"Privacy {spText.Error} {t1}/{t2}"));
        }

        public IActionResult TestException()
        {
            var i = 10;
            var d = 20 / (i - 10);
            return Content("Test");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                // log error.Error
            }

            return View(new ErrorViewModel("Error") {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}