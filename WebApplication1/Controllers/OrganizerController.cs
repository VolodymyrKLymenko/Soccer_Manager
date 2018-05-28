using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Model_Classes;
using WebApplication1.Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class OrganizerController : Controller
    {
        private readonly IHighLevelSoccerManagerService _highProvider;
        private readonly UserManager<DAL.Model_Classes.User> _userManager;

        private Team selectedTeam = null;
        private const string OrganaizerKey = "organizer";

        public Team SelectedTeam { get { return selectedTeam; } }

        public OrganizerController(IHighLevelSoccerManagerService high, UserManager<DAL.Model_Classes.User> userManager)
        {
            _highProvider = high;
            _userManager = userManager;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private async Task<Tournament> CurrentCup()
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            var tour = _highProvider.GetAllTournaments();
            return tour.FirstOrDefault(t => t.TournamentId == tournament_id);
        }

        public async Task<IActionResult> Index(int id = -1, string tabName = "")
        {
            Tournament tournament = await CurrentCup();
            List<Team> teams = _highProvider.GetAllTeam().ToList();


            selectedTeam = id != -1 ? _highProvider.GetTeam(id) : null;

            return View(new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam,
                    Teams = teams,
                    SelectedTeams = teams,
                    TabName = tabName
                });
        }

        [HttpPost]
        public async Task<IActionResult> SelectDate(string year = "all", int id = -1)
        {
            Tournament tournament = await CurrentCup();
            List<Team> teams = _highProvider.GetAllTeam().ToList();
            List<Team> selected_teams;

            if (year != "all")
            {
                selected_teams = teams.Where(el => el.DataCreation.Year == Int32.Parse(year)).ToList();
            }
            else
            {
                selected_teams = teams;
            }

            selectedTeam = id != -1 ? _highProvider.GetTeam(id) : null;

            return View("Index", new OrganaizerMainInfo()
            {
                Tournament = tournament,
                SelectedTeam = selectedTeam,
                Teams = teams,
                SelectedTeams = selected_teams
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            Tournament tournament = await CurrentCup();

            return View(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tournament _tournament)
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            Tournament tournament = await CurrentCup();

            _highProvider.UpdateTournament(tournament.TournamentId, _tournament);

            if (TempData != null && TempData.ContainsKey("message"))
            {
                TempData["message"] = $"{_tournament.Name} has been saved";
            }

            return RedirectToAction("Index", "Organizer");
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            Tournament tournament = _highProvider.GetAllTournaments().FirstOrDefault(t => t.TournamentId == tournament_id);

            return View("Index", new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam,
                    ShowConfirming = true
                });
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            Tournament tournament = await CurrentCup();

            _highProvider.RemoveTournament(tournament.TournamentId);
            var user = await GetCurrentUserAsync();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Logout", "Account");
        }

        public async Task<IActionResult> RemoveTeam(int TeamId)
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            Tournament tournament = await CurrentCup();

            _highProvider.RemoveTeamFromTournament(TeamId, tournament.TournamentId);

            TempData["message"] = $"{_highProvider.GetTeam(TeamId).Name} was removed";

            return View("Index",  new OrganaizerMainInfo()
                {
                    Tournament = tournament,
                    SelectedTeam = selectedTeam
                });
        }

    }
}