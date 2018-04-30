using DAL.Model_Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SoccerContext _dataContext;
        private readonly DbSet<TEntity> _dbset;


        public GenericRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();

            return -1;
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

        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where);
        }

        public IQueryable<TEntity> GetAll()
        {
            var lst = _dbset;
            return lst;
        }

        public void AddTeamTournaments(Team team, Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }

}
