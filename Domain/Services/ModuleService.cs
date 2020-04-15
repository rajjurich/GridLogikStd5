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
    public interface IModuleService
    {
        Task<mstmodule> Add(mstmodule entity);
        Task<IEnumerable<mstmodule>> AddRange(IEnumerable<mstmodule> entities);
        Task<mstmodule> Delete(mstmodule entity);
        Task<mstmodule> Edit(mstmodule entity);
        Task<mstmodule> Get(int Key);
        IQueryable<mstmodule> GetAll();
        Task<mstmodule> Remove(mstmodule entity);
        IQueryable<mstmodule> FindBy(Expression<Func<mstmodule, bool>> predicate);
        Task<IEnumerable<mstmodule>> RemoveRange(IEnumerable<mstmodule> entities);
    }

    public class ModuleService : IModuleService
    {
        IEntityRepository<mstmodule> _entityRepository;
        public ModuleService(IEntityRepository<mstmodule> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstmodule> Add(mstmodule entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstmodule>> AddRange(IEnumerable<mstmodule> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstmodule> Delete(mstmodule entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstmodule> Edit(mstmodule entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstmodule> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<mstmodule> GetAll()
        {
            return _entityRepository.GetAll();
        }

        public Task<mstmodule> Remove(mstmodule entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstmodule> FindBy(Expression<Func<mstmodule, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstmodule>> RemoveRange(IEnumerable<mstmodule> entities)
        {
            throw new NotImplementedException();
        }

    }
}
