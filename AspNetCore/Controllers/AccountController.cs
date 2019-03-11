using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Models;
using AspNetCore.Models.Input;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
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
            var principal = GetPrincipal(inputModel);
            if (principal != null)
            {
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                };
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //Sign the user in and create auth cookie
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginModel("Login Page")
                {ErrorMessage = "Invalid user name or password. Please try again.", InputModel = inputModel});
        }
        public async Task<IActionResult> Logout()
        {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
        }

        private ClaimsPrincipal GetPrincipal(LoginInputModel inputModel)
        {
            ClaimsPrincipal principal = null;
            if (inputModel.UserName.Contains("bal323"))
            {
                var clasims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, inputModel.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim("FullName", "Sergey Balog"),
                    new Claim("Module", "HR")
                };
                //create identity object from claims
                var identity =
                    new ClaimsIdentity(CookieAuthenticationDefaults
                        .AuthenticationScheme); 
                identity.AddClaims(clasims);
                //create principal object
                principal = new ClaimsPrincipal(identity);
            }
            return principal;
        }

        public async Task TwitterAuth()
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = "/account/external"
            };
            await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme, props);
        }

        public async Task<IActionResult> External()
        {
            var result = await HttpContext.AuthenticateAsync("TEMP");
            if (result.Succeeded)
            {
                var principal = result.Principal;
                List<Claim> claims = new List<Claim>();
                claims.AddRange(principal.Claims);
                claims.Add(new Claim("FullName", "Sergey Balog (Twitter)"));
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                var identity =
                    new ClaimsIdentity(CookieAuthenticationDefaults
                        .AuthenticationScheme); 
                identity.AddClaims(claims);
                //create principal object
                principal = new ClaimsPrincipal(identity);
                //Sign the user in and create auth cookie
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                };
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                await HttpContext.SignOutAsync("TEMP");
                return RedirectToAction("Index", "Home");
            }
            return View("Login", new LoginModel("Login Page")
                {ErrorMessage = "Invalid user name or password. Please try again."});
        }
    }
}