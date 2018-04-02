using Microsoft.EntityFrameworkCore;
using ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.DataAccess
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<TEntity> _dbset;

        private DbSet<Team> _dbsetTeam;
        private DbSet<Tournament> _dbsetTournament;

        public GenericRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Set<TEntity>();

            _dbsetTeam = _dataContext.Set<Team>();
            _dbsetTournament = _dataContext.Set<Tournament>();
        }

        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
            _dataContext.SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            _dbset.Where<TEntity>(where)
               .ForEachAsync(entity => _dbset.Remove(entity));
            _dataContext.SaveChanges();
        }

        public TEntity Get(long id)
        {
            return _dbset.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<TEntity>();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var lst = _dbset.ToList();
            return lst;
        }

        public IEnumerable<Team> GetTeamsWithDependecies()
        {
            return _dbsetTeam
                .Include(t => t.Players)
                .Include(t => t.TeamTournaments).ThenInclude(ts => ts.Tournaments)
                .ToList();
        }

        public IEnumerable<Tournament> GetTournamentsWithDependecies()
        {
            return _dbsetTournament
                .Include(t => t.TeamTournaments).ThenInclude(ts => ts.Teams)
                .ToList();
        }

        /*
        public void AddPlayerToTeam (int teamId, Player player)
        {
            Team team = _dbsetTeam
                .Include(t => t.Players)
                .Where(t => t.TeamId == teamId)
                .FirstOrDefault();

            team.Players.Add(player);
        }
        public void RemovePlayerToTeam(int teamId, Player player)
        {
            Team team = _dbsetTeam
                .Include(t => t.Players)
                .Where(t => t.TeamId == teamId)
                .FirstOrDefault();

            team.Players.Remove(player);
        }

        public void AddTeamToTournaments(int tournamentId, Team team)
        {
            Tournament tournament = _dbsetTournament
                .Include(t => t.Teams)
                .Where(t => t.TournamentId == tournamentId)
                .FirstOrDefault();

            tournament.Teams.Add(team);
        }
        public void RemoveTeamToTournaments(int tournamentId, Team team)
        {
            Tournament tournament = _dbsetTournament
                .Include(t => t.Teams)
                .Where(t => t.TournamentId == tournamentId)
                .FirstOrDefault();

            tournament.Teams.Remove(team);
        }

        public void AddTournamentToTeams(int teamId, Tournament tournament)
        {
            Team team = _dbsetTeam
                .Include(t => t.Tournaments)
                .Where(t => t.TeamId == teamId)
                .FirstOrDefault();

            team.Tournaments.Add(tournament);
        }
        public void RemoveTournamentToTeams(int teamId, Tournament tournament)
        {
            Team team = _dbsetTeam
                .Include(t => t.Tournaments)
                .Where(t => t.TeamId == teamId)
                .FirstOrDefault();

            team.Tournaments.Remove(tournament);
        }*/

    }

}