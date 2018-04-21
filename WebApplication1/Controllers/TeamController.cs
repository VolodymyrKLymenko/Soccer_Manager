using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Model_Classes;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using Services;
using WebApplication1.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
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
                Team = team,
                Cups = highProvider.GetAllTournaments().ToList()
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
                Team = _team,
                Cups = highProvider.GetAllTournaments().ToList()
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

        [HttpGet]
        public IActionResult EditPlayer(int playerId)
        {
            Player player = lowProvider.GetPlayer(playerId);

            return View(player);
        }

        [HttpPost]
        public IActionResult EditPlayer(Player player)
        {
            lowProvider.UpdatePlayer(player.PlayerId, player);

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
                ShowConfirming = false,
                Cups = highProvider.GetAllTournaments().ToList()
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
                ShowConfirming = true,
                Cups = highProvider.GetAllTournaments().ToList()
            });
        }

        [HttpGet]
        public IActionResult Delete(int TeamId, string Password)
        {
            Team team = highProvider.GetTeam(TeamId);

            if (team.Password == Password)
            {
                foreach(var i in team.Players)
                {
                    lowProvider.RemovePlayer(i.PlayerId);
                }
                highProvider.RemoveTeam(TeamId);
                HttpContext.Session.Remove(TeamKey);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveCup(int CupId, string Password)
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            if (Password == team.Password)
            {
                highProvider.RemoveTeamFromTournament(team.TeamId, CupId);
            }

            return RedirectToAction("Index", "Team");
        }

        public IActionResult RegistrToCup(int CupId, string Password)
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            if (Password == team.Password)
            {
                highProvider.AddTeamToTournament(team.TeamId, CupId);
            }

            return RedirectToAction("Index", "Team");
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
