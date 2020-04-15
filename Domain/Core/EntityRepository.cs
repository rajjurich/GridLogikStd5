using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
        Task<T> Delete(T entity);
        Task<T> Edit(T entity);
        Task<T> Get(int Key);
        IQueryable<T> GetAll();
        Task<T> Remove(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entities);
    }
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        readonly DbContext _entitiesContext;
        public EntityRepository(DbContext entitiesContext)
        {
            _entitiesContext = entitiesContext;
        }
        public async Task<T> Add(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            _entitiesContext.Set<T>().Add(entity);
            await _entitiesContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {            
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            await _entitiesContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            await _entitiesContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(int Key)
        {
            return await _entitiesContext.Set<T>().FindAsync(Key);
        }

        public IQueryable<T> GetAll()
        {
            return _entitiesContext.Set<T>();
        }

        public async Task<T> Remove(T entity)
        {
            //T entity = await Get(Key);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
            await _entitiesContext.SaveChangesAsync();
            return entity;
        }


        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            _entitiesContext.Set<T>().AddRange(entities);
            await _entitiesContext.SaveChangesAsync();
            return entities;
        }


        public async Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entities)
        {
            _entitiesContext.Set<T>().RemoveRange(entities);            
            await _entitiesContext.SaveChangesAsync();
            return entities;
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entitiesContext.Set<T>().Where(predicate);
        }
    }
}
