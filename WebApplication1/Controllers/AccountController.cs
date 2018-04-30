using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Model_Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHighLevelSoccerManagerService _highProvider;

        public AccountController(IHighLevelSoccerManagerService high)
        {
            _highProvider = high;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserType == UserType.Organizer)
                {
                    Tournament user = await _highProvider.GetAllTournaments().FirstOrDefaultAsync(u => u.Name == model.Name);
                    if (user != null)
                    {
                        await Authenticate(model.Name); // аутентифiкацiя
    
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else if (model.UserType == UserType.Team)
                {
                    Team user = await _highProvider.GetAllTeam().FirstOrDefaultAsync(u => u.Name == model.Name);
                    if (user != null)
                    {
                        await Authenticate(model.Name); // аутентифiкацiя

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public /*async Task<IActionResult>*/void Register(RegisterModel model)
        {
           /* if (ModelState.IsValid)
            {
                if(model.)
                User user = await _highProvider.GetAllTournaments().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);*/
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }


}