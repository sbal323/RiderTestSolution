using AspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [AllowAnonymous]
    public class AccountController:Controller
    {
        public IActionResult Login()
        {
            return View(new BaseModel("Login Page"));
        }
    }
}