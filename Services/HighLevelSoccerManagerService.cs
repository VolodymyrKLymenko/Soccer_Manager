﻿using System;
using ModelClasses;
using DAL.DataAccess;

namespace Services
{

    public interface IHighLevelTestManagementService
    {
        void CreateTeam(Team team);
        void UpdateTeam(int teamId, Team updatedTeam);
        void RemoveTeam(int teamId);

        void CreateTournament(Tournament tournament);
        void UpdateTournament(int tournamentId, Tournament updatedTournament);
        void RemoveTournament(int tournamentId);
    }

    public class HighLevelSoccerManagerService : IHighLevelTestManagementService
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

        public void RemoveTeam(int teamId)
        {
            _teamRepository.Delete(team => team.TeamId == teamId);
        }

        public void RemoveTournament(int tournamentId)
        {
            _tournamentRepository.Delete(tournament => tournament.TournamentId == tournamentId);
        }

        public void UpdateTeam(int teamId, Team updatedTeam)
        {
            var teamFromDb = _teamRepository.Get(team => team.TeamId == teamId);

            teamFromDb.Name = updatedTeam.Name;

            _teamRepository.Update(teamFromDb);
        }

        public void UpdateTournament(int tournamentId, Tournament updatedTournament)
        {
            var tournamentFromDb = _tournamentRepository.Get(t => t.TournamentId == tournamentId);

            tournamentFromDb.Name = updatedTournament.Name;
            tournamentFromDb.MaxCountTeams = updatedTournament.MaxCountTeams;
            tournamentFromDb.StartDate = updatedTournament.StartDate;
            tournamentFromDb.EndDate = updatedTournament.EndDate;

            _tournamentRepository.Update(tournamentFromDb);
        }
    }

}
