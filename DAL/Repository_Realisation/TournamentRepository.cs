using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Model_Classes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository_Realisation
{
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<Tournament> _dbset;


        public TournamentRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Tournaments;
        }

        public int Add(Tournament item)
        {
            var t = _dbset.Add(item);
            var db = _dbset.ToList();

            _dataContext.SaveChanges();

            return t.Entity.TournamentId;
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

        public IQueryable<Tournament> GetMany(Expression<Func<Tournament, bool>> where)
        {
            var cups = _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team);

            return cups.Where(where);
        }

        public IQueryable<Tournament> GetAll()
        {
            return _dbset.Include(t => t.TeamTournaments).ThenInclude(tt => tt.Team);
        }
    }
}
