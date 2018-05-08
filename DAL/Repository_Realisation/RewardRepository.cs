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
    public class RewardRepository : IRepository<Reward>
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<Reward> _dbset;


        public RewardRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Rewards;
        }

        public int Add(Reward item)
        {
            var t = _dbset.Add(item);
            _dataContext.SaveChanges();

            return t.Entity.RewardId;
        }

        public void Update(Reward item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Delete(Reward item)
        {
            _dbset.Remove(item);
            _dataContext.SaveChanges();
        }

        public void Delete(Expression<Func<Reward, bool>> where)
        {
            _dbset.Where(where)
               .ForEachAsync(entity => _dbset.Remove(entity));
            _dataContext.SaveChanges();
        }

        public Reward Get(long id)
        {
            var Rewards = _dbset.Include(t => t.Team);

            return Rewards.First(t => t.RewardId == id);
        }

        public Reward Get(Expression<Func<Reward, bool>> where)
        {
            var Rewards = _dbset.Include(t => t.Team);

            return Rewards.Where(where).FirstOrDefault<Reward>();
        }

        public IQueryable<Reward> GetMany(Expression<Func<Reward, bool>> where)
        {
            var Rewards = _dbset.Include(t => t.Team);

            return Rewards.Where(where);
        }

        public IQueryable<Reward> GetAll()
        {
            return _dbset.Include(t => t.Team);
        }

        public void AddTeamTournaments(Team team, Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }

}
