using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Model_Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private static string TeamRole = "Team";
        private static string OrgRole = "Organizer";
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
                        await Authenticate(model.Name, OrgRole); // аутентифiкацiя

                        TempData["message"] = $"You have been logged as Organizer: {user.Name}";

                        return RedirectToAction("Index", "Organizer");
                    }
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else if (model.UserType == UserType.Team)
                {
                    Team user = await _highProvider.GetAllTeam().FirstOrDefaultAsync(u => u.Name == model.Name);
                    if (user != null)
                    {
                        await Authenticate(model.Name, TeamRole); // аутентифiкацiя

                        TempData["message"] = $"You have been logged as Team: {user.Name}";

                        return RedirectToAction("Index", "Team");
                    }
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterTeam()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTeam(WebApplication1.Models.ViewModels.TeamModels.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Team user = await _highProvider.GetAllTeam().FirstOrDefaultAsync(u => u.Name == model.Name);
                if (user == null)
                {
                    _highProvider.CreateTeam(new Team { Name = model.Name, Mail = model.Email, Password = model.Password });

                    await Authenticate(model.Name, "Team");

                    TempData["message"] = $"You have been created new team: {model.Name}";

                    return RedirectToAction("Index", "Team");
                }
                else
                    ModelState.AddModelError("", "Incorrect data");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterCup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCup(WebApplication1.Models.ViewModels.OrganizerModels.RegisterOrganizerModel model)
        {
            if (ModelState.IsValid)
            {
                Tournament user = await _highProvider.GetAllTournaments().FirstOrDefaultAsync(u => u.Name == model.Name);
                if (user == null)
                {
                    _highProvider.CreateTournament(new Tournament { Name = model.Name, Mail = model.Email, Password = model.Password,
                                                                    StartDate = model.StartDate, EndDate = model.EndDate, MaxCountTeams = model.MaxCountTeam});

                    await Authenticate(model.Name, OrgRole);

                    TempData["message"] = $"You have been created new tournament: {model.Name}";

                    return RedirectToAction("Index", "Organizer");
                }
                else
                    ModelState.AddModelError("", "Incorrect data");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }


}