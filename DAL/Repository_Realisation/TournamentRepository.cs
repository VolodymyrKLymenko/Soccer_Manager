using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using DAL;
using DAL.Model_Classes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository_Realisation
{
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<Tournament> _dbset;
        private readonly DbSet<Team> _dbsetTeams;


        public TournamentRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Tournaments;
            _dbsetTeams = _dataContext.Teams;
        }

        public void Add(Tournament item)
        {
            _dbset.Add(item);
            _dataContext.SaveChanges();
        }

        public void Update(Tournament item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Delete(Tournament item)
        {
            _dbset.Remove(item);
            _dataContext.SaveChanges();
        }

        public void Delete(Expression<Func<Tournament, bool>> where)
        {
            _dbset.Where(where)
               .ForEachAsync(entity => _dbset.Remove(entity));
            _dataContext.SaveChanges();
        }

        public Tournament Get(long id)
        {
            var cups = _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team);

            return cups.FirstOrDefault(t => t.TournamentId == id);
        }

        public Tournament Get(Expression<Func<Tournament, bool>> where)
        {
            var cups = _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team);

            return cups.Where(where).FirstOrDefault<Tournament>();
        }

        public IEnumerable<Tournament> GetMany(Expression<Func<Tournament, bool>> where)
        {
            var cups = _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team);

            return cups.Where(where).ToList();
        }

        public IEnumerable<Tournament> GetAll()
        {
            return _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team).ToList();
        }

        public void AddTeamTournaments(Team team, Tournament tournament)
        {
            Tournament cup = _dbset.Where(t => t.TournamentId == tournament.TournamentId).FirstOrDefault();
            Team _team = _dbsetTeams.Where(tt => tt.TeamId == team.TeamId).FirstOrDefault();

            if (cup != null)
            {
                cup.TeamTournaments.Add(new TeamTournament() { Team = _team, Tournament = cup });
            }
            _dataContext.SaveChanges();
        }
    }
}
