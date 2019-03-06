using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Authorize]
    public class SecureController: Controller
    {
        public IActionResult Data()
        {
            return Content("secure data");
        }
    }
}