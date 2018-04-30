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
    public class PlayerRepository: IRepository<Player>
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<Player> _dbset;


        public PlayerRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Players;
        }

        public int Add(Player item)
        {
            var t = _dbset.Add(item);
            _dataContext.SaveChanges();

            return t.Entity.PlayerId;
        }

        public void Update(Player item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Delete(Player item)
        {
            _dbset.Remove(item);
            _dataContext.SaveChanges();
        }

        public void Delete(Expression<Func<Player, bool>> where)
        {
            _dbset.Where(where)
               .ForEachAsync(entity => _dbset.Remove(entity));
            _dataContext.SaveChanges();
        }

        public Player Get(long id)
        {
            var players = _dbset.Include(t => t.Team);

            return players.First(t => t.PlayerId == id);
        }

        public Player Get(Expression<Func<Player, bool>> where)
        {
            var players = _dbset.Include(t => t.Team);

            return players.Where(where).FirstOrDefault<Player>();
        }

        public IQueryable<Player> GetMany(Expression<Func<Player, bool>> where)
        {
            var players = _dbset.Include(t => t.Team);

            return players.Where(where);
        }

        public IQueryable<Player> GetAll()
        {
            return  _dbset.Include(t => t.Team);
        }
    }

}
