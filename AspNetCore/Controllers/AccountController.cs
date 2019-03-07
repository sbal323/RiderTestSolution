using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Models;
using AspNetCore.Models.Input;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [AllowAnonymous]
    public class AccountController:Controller
    {
        public IActionResult Login()
        {
            return View(new LoginModel("Login Page"));
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (inputModel.UserName.Contains("bal323"))
            {
                var clasims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,inputModel.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim("FullName","Sergey Balog")
                };
                //create identity object from claims
                var identity = new ClaimsIdentity(clasims,CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                //create principal object
                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
//                    AllowRefresh = true,
//                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
//                    IsPersistent = true,
                };
                //Sign the user in and create auth cookie
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginModel("Login Page"){ErrorMessage = "Invalid user name or password. Please try again.", InputModel = inputModel});
        }
    }
}