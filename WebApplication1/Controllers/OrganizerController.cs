using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Model_Classes;
using WebApplication1.Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class OrganizerController : Controller
    {
        private readonly IHighLevelSoccerManagerService highProvider;

        private Team selectedTeam = null;
        private const string OrganaizerKey = "organizer";

        public OrganizerController(IHighLevelSoccerManagerService high)
        {
            highProvider = high;
        }

        public IActionResult Index(int id = -1)
        {
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);

            selectedTeam = id != -1 ? highProvider.GetTeam(id) : null;

            return View(new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam
                });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);

            return View(tournament);
        }

        [HttpPost]
        public IActionResult Edit(Tournament _tournament)
        {
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);
            highProvider.UpdateTournament(tournament.TournamentId, _tournament);
            tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);

            TempData["message"] = $"{_tournament.Name} has been saved";

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
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);

            return View("Index", new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam,
                    ShowConfirming = true
                });
        }

        [HttpGet]
        public IActionResult Delete()
        {
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);
            highProvider.RemoveTournament(tournament.TournamentId);

            return RedirectToAction("Logout", "Account");
        }

        public IActionResult RemoveTeam(int TeamId)
        {
            Tournament tournament = highProvider.GetAllTournaments().FirstOrDefault(t => t.Name == User.Identity.Name);
            highProvider.RemoveTeamFromTournament(TeamId, tournament.TournamentId);

            TempData["message"] = $"{highProvider.GetTeam(TeamId).Name} was removed";

            return View("Index",  new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam
                });
        }

    }
}