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
    public interface IUserService
    {
        Task<mstuser> Add(mstuser entity);
        Task<IEnumerable<mstuser>> AddRange(IEnumerable<mstuser> entities);
        Task<mstuser> Delete(mstuser entity);
        Task<mstuser> Edit(mstuser entity);
        Task<mstuser> Get(int Key);
        IQueryable<mstuser> GetAll();
        Task<mstuser> Remove(mstuser entity);
        IQueryable<mstuser> FindBy(Expression<Func<mstuser, bool>> predicate);
        Task<IEnumerable<mstuser>> RemoveRange(IEnumerable<mstuser> entities);
    }
    public class UserService : IUserService
    {
        IEntityRepository<mstuser> _entityRepository;
        public UserService(IEntityRepository<mstuser> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public Task<mstuser> Add(mstuser entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstuser>> AddRange(IEnumerable<mstuser> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstuser> Delete(mstuser entity)
        {
            entity.usrisdeleted = 1;
            return _entityRepository.Delete(entity);
        }

        public Task<mstuser> Edit(mstuser entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstuser> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<mstuser> GetAll()
        {
            return _entityRepository.GetAll().Where(x=>x.usrisdeleted == null || x.usrisdeleted == 0);
        }

        public Task<mstuser> Remove(mstuser entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstuser> FindBy(Expression<Func<mstuser, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstuser>> RemoveRange(IEnumerable<mstuser> entities)
        {
            throw new NotImplementedException();
        }
    }
}
