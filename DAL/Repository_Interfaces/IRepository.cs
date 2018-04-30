using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DAL.Model_Classes;

namespace DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
        TEntity Get(long id);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetAll();
    }
}
