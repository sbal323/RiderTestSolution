using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Areas.Music.Controllers
{
    [Area("Music")]
    public class HomeController:Controller
    {
        public IActionResult Index(string page)
        {
            return Json(new {Data = "Inside home controller", Area = "Music"});
        }
    }
}