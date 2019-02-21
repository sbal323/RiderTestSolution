using System;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Controller]
    public class Pono
    {
        public IActionResult Data()
        {
            return new JsonResult(new {Day = DateTime.Today, Id = 234});
        }
    }
}