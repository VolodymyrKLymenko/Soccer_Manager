using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Model_Classes;
using WebApplication1.Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Team")]
    public class TeamController : Controller
    {
        private IHighLevelSoccerManagerService highProvider;
        private ILowLevelSoccerManagmentService lowProvider;

        private const string TeamKey = "team";

        public TeamController(IHighLevelSoccerManagerService high, ILowLevelSoccerManagmentService low)
        {
            highProvider = high;
            lowProvider = low;
        }

        public IActionResult Index()
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            return View(new TeamMainInfo()
            {
                Team = team
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team _team)
        {
            highProvider.CreateTeam(_team);

            if (_team != null && _team.Password == _team.Password)
            {
                HttpContext.Session.SetInt32(TeamKey, _team.TeamId);
            }

            return RedirectToAction("Index", new TeamMainInfo()
            {
                Team = _team
            });
        }

        [HttpGet]
        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            lowProvider.CreatePlayerForTeam(team.TeamId, player);
            highProvider.UpdateTeam(team.TeamId, team);

            return RedirectToAction("Index");
        }

        public IActionResult RemovePlayer(int PlayerId, string Password)
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            if (Password == team.Password)
            {
                lowProvider.RemovePlayer(PlayerId);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(Team team)
        {
            highProvider.UpdateTeam(team.TeamId, team);

            var value = HttpContext.Session.GetInt32(TeamKey);
            Team _team = value != null ? highProvider.GetTeam(value.Value) : null;

            return View("Index", new TeamMainInfo()
            {
                Team = _team,
                ShowConfirming = false
            });
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            return View("Index", new TeamMainInfo()
            {
                Team = team,
                ShowConfirming = true
            });
        }

        [HttpGet]
        public IActionResult Delete(int TeamId, string Password)
        {
            Team team = highProvider.GetTeam(TeamId);

            if (team.Password == Password)
            {
                highProvider.RemoveTeam(TeamId);
                HttpContext.Session.Remove(TeamKey);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginCupModel model)
        {
            var team = highProvider.GetAllTeam().FirstOrDefault(t => t.Name == model.Name);

            if (team != null && team.Password == model.Password)
            {
                HttpContext.Session.SetInt32(TeamKey, team.TeamId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove(TeamKey);

            return RedirectToAction("Index");
        }
    }
}
