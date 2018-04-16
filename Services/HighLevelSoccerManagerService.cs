using System.Collections.Generic;
using DAL;
using DAL.Model_Classes;

namespace Services
{
    public interface IHighLevelSoccerManagerService
    {
        void CreateTeam(Team team);
        void UpdateTeam(int teamId, Team updatedTeam);
        void RemoveTeam(int teamId);
        void RemoveTeamFromTournament(int teamId, int tournamentId);
        Team GetTeam(int teamId);
        IEnumerable<Team> GetAllTeam();
        IEnumerable<Tournament> GetAllTournaments();

        void CreateTournament(Tournament tournament);
        void UpdateTournament(int tournamentId, Tournament updatedTournament);
        void RemoveTournament(int tournamentId);
        Tournament GetTournament(int id);
    }

    public class HighLevelSoccerManagerService : IHighLevelSoccerManagerService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<Team> _teamRepository;

        public HighLevelSoccerManagerService(IRepository<Tournament> tournamentRepository, IRepository<Team> teamRepository)
        {
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
        }


        public void CreateTeam(Team team)
        {
            _teamRepository.Add(team);
        }

        public void CreateTournament(Tournament tournament)
        {
            _tournamentRepository.Add(tournament);
        }

        public IEnumerable<Team> GetAllTeam()
        {
            return _teamRepository.GetAll();
        }

        public Team GetTeam(int teamId)
        {
            return _teamRepository.Get(teamId);
        }

        public void RemoveTeam(int teamId)
        {
            _teamRepository.Delete(team => team.TeamId == teamId);
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return _tournamentRepository.GetAll();
        }

        public Tournament GetTournament(int id)
        {
            return _tournamentRepository.Get(id);
        }

        public void RemoveTournament(int tournamentId)
        {
            _tournamentRepository.Delete(tournament => tournament.TournamentId == tournamentId);
        }

        public void UpdateTeam(int teamId, Team updatedTeam)
        {
            var teamFromDb = _teamRepository.Get(team => team.TeamId == teamId);

            teamFromDb.Name = updatedTeam.Name;
            teamFromDb.Mail = updatedTeam.Mail;

            _teamRepository.Update(teamFromDb);
        }

        public void UpdateTournament(int tournamentId, Tournament updatedTournament)
        {
            var tournamentFromDb = _tournamentRepository.Get(t => t.TournamentId == tournamentId);

            tournamentFromDb.Name = updatedTournament.Name;
            tournamentFromDb.Mail = updatedTournament.Mail;
            tournamentFromDb.MaxCountTeams = updatedTournament.MaxCountTeams;
            tournamentFromDb.StartDate = updatedTournament.StartDate;
            tournamentFromDb.EndDate = updatedTournament.EndDate;

            _tournamentRepository.Update(tournamentFromDb);
        }

        public void RemoveTeamFromTournament(int teamId, int tournamentId)
        {
            var tournamentFromDb = _tournamentRepository.Get(t => t.TournamentId == tournamentId);
            tournamentFromDb.TeamTournaments.RemoveAll(tt => tt.TournamentId == teamId);

            _tournamentRepository.Update(tournamentFromDb);

        }
    }

}
