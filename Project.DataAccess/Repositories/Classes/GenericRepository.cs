using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Data.Contexts;
using Project.DataAccess.Models.DepartmentModel;
using Project.DataAccess.Models.Shared;
using Project.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Project.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //CRUD operations
        //Get By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        //Get All
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
        {
            return _dbContext.Set<TEntity>()
                .Where(e => !e.IsDeleted)
                .Select(Selector)
                .ToList();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(Predicate)
                .ToList();
        }

        //Insert
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        //Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        //Delete
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

    }
}
