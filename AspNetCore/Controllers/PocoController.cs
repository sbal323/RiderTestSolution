using System;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
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