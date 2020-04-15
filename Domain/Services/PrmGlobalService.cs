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
    public interface IPrmGlobalService
    {
        Task<prmglobal> Add(prmglobal entity);
        Task<IEnumerable<prmglobal>> AddRange(IEnumerable<prmglobal> entities);
        Task<prmglobal> Delete(prmglobal entity);
        Task<prmglobal> Edit(prmglobal entity);
        Task<prmglobal> Get(int Key);
        IQueryable<prmglobal> GetAll();
        Task<prmglobal> Remove(prmglobal entity);
        IQueryable<prmglobal> FindBy(Expression<Func<prmglobal, bool>> predicate);
        Task<IEnumerable<prmglobal>> RemoveRange(IEnumerable<prmglobal> entities);
    }
    public class PrmGlobalService : IPrmGlobalService
    {
        IEntityRepository<prmglobal> _entityRepository;
        IUnitOfWork _entity;

        public PrmGlobalService(IEntityRepository<prmglobal> entityRepository, IUnitOfWork entity)
        {
            _entityRepository = entityRepository;
            _entity = entity;

        }

        public Task<prmglobal> Add(prmglobal entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<prmglobal>> AddRange(IEnumerable<prmglobal> entities)
        {
            throw new NotImplementedException();
        }

        public Task<prmglobal> Delete(prmglobal entity)
        {            
            return _entityRepository.Delete(entity);
        }

        public Task<prmglobal> Edit(prmglobal entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<prmglobal> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<prmglobal> GetAll()
        {
            return _entityRepository.GetAll();
        }

        public Task<prmglobal> Remove(prmglobal entity)
        {
            return _entityRepository.Remove(entity);
        }

        public IQueryable<prmglobal> FindBy(Expression<Func<prmglobal, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<prmglobal>> RemoveRange(IEnumerable<prmglobal> entities)
        {
            throw new NotImplementedException();
        }
    }
}
