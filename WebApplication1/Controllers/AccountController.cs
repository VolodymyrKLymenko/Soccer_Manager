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
        private readonly IHighLevelSoccerManagerService _highProvider;
        private readonly UserManager<DAL.Model_Classes.User> _userManager;
        private readonly SignInManager<DAL.Model_Classes.User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IHighLevelSoccerManagerService high, 
            UserManager<DAL.Model_Classes.User> userManager, 
            SignInManager<DAL.Model_Classes.User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _highProvider = high;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                    var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Organizer");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Login or password");
                        return View(model);
                    }
                }
                else if (model.UserType == UserType.Team)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Team");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Login or password");
                        return View(model);
                    }
                }
            }

            return View();
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
                int id = _highProvider.CreateTeam(new Team { Name = model.Name, Mail = model.Email, Password = model.Password, DataCreation = model.DataCreation });

                DAL.Model_Classes.User user = new DAL.Model_Classes.User { UserId = id, UserName = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
 
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Team");
                    if (TempData != null)
                    {
                        TempData["message"] = $"You have been created new team: {model.Name}";
                    
                        await _signInManager.SignInAsync(user, false);
                    }
                    return RedirectToAction("Index", "Team");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
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
                int id = _highProvider.CreateTournament(new Tournament { Name = model.Name, Mail = model.Email, Password = model.Password,
                    StartDate = model.StartDate, EndDate = model.EndDate, MaxCountTeams = model.MaxCountTeam});

                DAL.Model_Classes.User user = new DAL.Model_Classes.User { UserId = id, UserName = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Organizer");
                    if (TempData != null)
                    {
                        TempData["message"] = $"You have been created new tournament: {model.Name}";
                        await _signInManager.SignInAsync(user, false);
                    }
                    return RedirectToAction("Index", "Organizer");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }


}