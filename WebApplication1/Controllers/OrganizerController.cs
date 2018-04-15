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
    public class OrganizerController : Controller
    {
        private IHighLevelSoccerManagerService highProvider;
        private ILowLevelSoccerManagmentService lowProvider;

        private Team selectedTeam = null;

        private const string OrganaizerKey = "organizer";

        public OrganizerController(IHighLevelSoccerManagerService high
            , ILowLevelSoccerManagmentService low)
        {
            highProvider = high;
            lowProvider = low;
        }

        public IActionResult Index(int id = -1)
        {
            var value = HttpContext.Session.GetInt32(OrganaizerKey);
            Tournament tournament = value!=null ? highProvider.GetTournament(value.Value) : null;

            selectedTeam = id != -1 ? highProvider.GetTeam(id) : null;

            return View(new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam
                });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tournament _tournament)
        {
            highProvider.CreateTournament(_tournament);

            if (_tournament != null && _tournament.Password == _tournament.Password)
            {
                HttpContext.Session.SetInt32(OrganaizerKey, _tournament.TournamentId);
            } 

            return RedirectToAction("Index", new OrganaizerMainInfo()
                {
                    Tournament = _tournament,
                    SelectedTeam = selectedTeam
                });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var value = HttpContext.Session.GetInt32(OrganaizerKey);
            Tournament tournament = value != null ? highProvider.GetTournament(value.Value) : null;

            return View(tournament);
        }

        [HttpPost]
        public IActionResult Edit(Tournament tournament)
        {
            highProvider.UpdateTournament(tournament.TournamentId, tournament);

            return View("Index", new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam,
                    ShowConfirming = false
                });
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            var value = HttpContext.Session.GetInt32(OrganaizerKey);
            Tournament tournament = value != null ? highProvider.GetTournament(value.Value) : null;

            return View("Index", new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam,
                    ShowConfirming = true
                });
        }

        [HttpGet]
        public IActionResult Delete(int TournamentId, string Password)
        {
            Tournament tournament = highProvider.GetTournament(TournamentId);

            if(tournament.Password == Password)
            {
                highProvider.RemoveTournament(TournamentId);
                HttpContext.Session.Remove(OrganaizerKey);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveTeam(int TeamId, string Password)
        {
            var value = HttpContext.Session.GetInt32(OrganaizerKey);
            Tournament tournament = value != null ? highProvider.GetTournament(value.Value) : null;

            if(Password == tournament.Password)
            {
                highProvider.RemoveTeamFromTournament(TeamId, tournament.TournamentId);
            }

            return View("Index",  new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam
                });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginCupModel model)
        {
            var tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == model.Name);

            if (tournament != null && tournament.Password == model.Password)
            {
                HttpContext.Session.SetInt32(OrganaizerKey, tournament.TournamentId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove(OrganaizerKey);

            return RedirectToAction("Index");
        }

    }
}