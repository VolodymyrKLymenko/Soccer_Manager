using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ViewModels;
using DAL;
using DAL.Model_Classes;
using WebApplication1.Models;
using Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHighLevelSoccerManagerService _highService;
        private readonly ILowLevelSoccerManagmentService  _lowService;

        public HomeController(IHighLevelSoccerManagerService high,
            ILowLevelSoccerManagmentService low)
        {
            _highService = high;
            _lowService = low;
        }

        public IActionResult Index(string searchString)
        {
            GeneralInfo general = new GeneralInfo();
            general.Players = _lowService.GetAllPlayers().ToList();
            general.Teams = _highService.GetAllTeam().ToList();
            general.Tournaments = _highService.GetAllTournaments().ToList();

            List<Team> _teams = new List<Team>();

            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var r in _lowService.GetAllRewards())
                {
                    if (r.Name.Contains(searchString) && !_teams.Contains(_highService.GetTeam(r.Team.TeamId)))
                    {
                        _teams.Add(_highService.GetTeam(r.Team.TeamId));
                    }
                }
                general.Teams = _teams;
            }

            general.RecalculateAge();

            return View(general);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Team(int id)
        {
            Team t = _highService.GetTeam(id);

            return View("Team", t);
        }

        public IActionResult Cup(int id)
        {
            Tournament t = _highService.GetTournament(id);

            return View("Cup", t);
        }

        public IActionResult Error()
        {
            if (HttpContext != null)
            {
                return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
            }
            else
            {
                return View(new ErrorViewModel());
            }
        }
    }
}
