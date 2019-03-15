using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Authorize]
    public class TestController:Controller
    {
        [Route("/test/{id}")]
        public IActionResult Index(int id)
        {
            return Json(new {Id = id, Status = "Online"});
        }
    }
}