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

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private SoccerContext provider;

        public HomeController(SoccerContext _provider)
        {
            provider = _provider;
        }

        public IActionResult Index()
        {
            GeneralInfo general = new GeneralInfo();
            general.Players = provider.Players.ToList();
            general.Teams = provider.Teams.ToList();
            general.Tournaments = provider.Tournaments.ToList();

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



        public IActionResult Cup(int id)
        {
            Tournament t = provider.Tournaments.First((tourn) => tourn.TournamentId == id);

            return View("Cup", t);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
