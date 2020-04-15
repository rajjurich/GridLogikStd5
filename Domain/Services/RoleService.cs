using Domain.Core;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IRoleService
    {
        Task<mstrole> Add(mstrole entity);
        Task<IEnumerable<mstrole>> AddRange(IEnumerable<mstrole> entities);
        Task<mstrole> Delete(mstrole entity);
        Task<mstrole> Edit(mstrole entity);
        Task<mstrole> Get(int Key);
        IQueryable<mstrole> GetAll();
        Task<mstrole> Remove(mstrole entity);
        IQueryable<mstrole> FindBy(Expression<Func<mstrole, bool>> predicate);
        Task<IEnumerable<mstrole>> RemoveRange(IEnumerable<mstrole> entities);
    }
    public class RoleService : IRoleService
    {
        IEntityRepository<mstrole> entityRepository;
        public RoleService(IEntityRepository<mstrole> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<mstrole> Add(mstrole entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstrole>> AddRange(IEnumerable<mstrole> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstrole> Delete(mstrole entity)
        {
            entity.rolisdeleted = 1;
            return entityRepository.Edit(entity);
        }

        public Task<mstrole> Edit(mstrole entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<mstrole> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<mstrole> GetAll()
        {
            return entityRepository.GetAll().Where(x => x.rolisdeleted == 0 || x.rolisdeleted == null);
        }

        public Task<mstrole> Remove(mstrole entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstrole> FindBy(Expression<Func<mstrole, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstrole>> RemoveRange(IEnumerable<mstrole> entities)
        {
            throw new NotImplementedException();
        }
    }
}
