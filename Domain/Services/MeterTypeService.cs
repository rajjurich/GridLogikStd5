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
    public interface IMeterTypeService
    {
        Task<metertype> Add(metertype entity);
        Task<IEnumerable<metertype>> AddRange(IEnumerable<metertype> entities);
        Task<metertype> Delete(metertype entity);
        Task<metertype> Edit(metertype entity);
        Task<metertype> Get(int Key);
        IQueryable<metertype> GetAll();
        Task<metertype> Remove(metertype entity);
        IQueryable<metertype> FindBy(Expression<Func<metertype, bool>> predicate);
        Task<IEnumerable<metertype>> RemoveRange(IEnumerable<metertype> entities);
    }
    public class MeterTypeService : IMeterTypeService
    {
        IEntityRepository<metertype> _entityRepository;

        public MeterTypeService(IEntityRepository<metertype> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<metertype> Add(metertype entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<metertype>> AddRange(IEnumerable<metertype> entities)
        {
            throw new NotImplementedException();
        }

        public Task<metertype> Delete(metertype entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<metertype> Edit(metertype entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<metertype> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<metertype> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<metertype> Remove(metertype entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<metertype> FindBy(Expression<Func<metertype, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<metertype>> RemoveRange(IEnumerable<metertype> entities)
        {
            throw new NotImplementedException();
        }
    }
}
