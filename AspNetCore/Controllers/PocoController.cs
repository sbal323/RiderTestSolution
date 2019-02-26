using System;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Route("poco/[action]")]
    public class PocoController
    {
        public IActionResult Day()
        {
            return new ContentResult
            {
                Content = DateTime.Today.ToString("dd MMM yyyy")
            };
        }
    }
}