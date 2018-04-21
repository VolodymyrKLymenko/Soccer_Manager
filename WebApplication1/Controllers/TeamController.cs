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
            if (ModelState.IsValid)
            {
                highProvider.CreateTeam(_team);

                if (_team != null && _team.Password == _team.Password)
                {
                    HttpContext.Session.SetInt32(TeamKey, _team.TeamId);
                }

                TempData["message"] = $"{_team.Name} has been created";

                return RedirectToAction("Index", new TeamMainInfo()
                {
                    Team = _team,
                    Cups = highProvider.GetAllTournaments().ToList()
                });
            }
            else
            {
                return View();
            }
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

            if (ModelState.IsValid)
            {
                lowProvider.CreatePlayerForTeam(team.TeamId, player);
                highProvider.UpdateTeam(team.TeamId, team);
                TempData["message"] = $"{player.Name} has been added";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(player);
            }
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
            if (ModelState.IsValid)
            {
                lowProvider.UpdatePlayer(player.PlayerId, player);
                TempData["message"] = $"{player.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(player);
            }
        }

        public IActionResult RemovePlayer(int PlayerId, string Password)
        {
            var value = HttpContext.Session.GetInt32(TeamKey);
            Team team = value != null ? highProvider.GetTeam(value.Value) : null;

            if (Password == team.Password)
            {
                TempData["message"] = $"{lowProvider.GetPlayer(PlayerId).Name} was removed";
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
            if (ModelState.IsValid)
            {
                highProvider.UpdateTeam(team.TeamId, team);

                var value = HttpContext.Session.GetInt32(TeamKey);
                Team _team = value != null ? highProvider.GetTeam(value.Value) : null;
                
                TempData["message"] = $"{_team.Name} has been saved";

                return View("Index", new TeamMainInfo()
                {
                    Team = _team,
                    ShowConfirming = false,
                    Cups = highProvider.GetAllTournaments().ToList()
                });
            }
            else
            {
                return View(team);
            }
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
                TempData["message"] = $"{highProvider.GetTournament(CupId).Name} was removed";
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
                TempData["message"] = $"You have been registr for the {highProvider.GetTournament(CupId).Name}";
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
            if (ModelState.IsValid)
            {
                var team = highProvider.GetAllTeam().FirstOrDefault(t => t.Name == model.Name);

                if (team != null && team.Password == model.Password)
                {
                    HttpContext.Session.SetInt32(TeamKey, team.TeamId);
                }

                TempData["message"] = $"You have been logged as {team.Name}";

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove(TeamKey);

            return RedirectToAction("Index");
        }
    }
}
