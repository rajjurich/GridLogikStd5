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
    public interface IMeterModelService
    {
        Task<metermodel> Add(metermodel entity);
        Task<IEnumerable<metermodel>> AddRange(IEnumerable<metermodel> entities);
        Task<metermodel> Delete(metermodel entity);
        Task<metermodel> Edit(metermodel entity);
        Task<metermodel> Get(int Key);
        IQueryable<metermodel> GetAll();
        Task<metermodel> Remove(metermodel entity);
        IQueryable<metermodel> FindBy(Expression<Func<metermodel, bool>> predicate);
        Task<IEnumerable<metermodel>> RemoveRange(IEnumerable<metermodel> entities);
    }
    public class MeterModelService : IMeterModelService
    {
        IEntityRepository<metermodel> _entityRepository;
        public MeterModelService(IEntityRepository<metermodel> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public Task<metermodel> Add(metermodel entity)
        {            
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<metermodel>> AddRange(IEnumerable<metermodel> entities)
        {
            throw new NotImplementedException();
        }

        public Task<metermodel> Delete(metermodel entity)
        {
            entity.isdeleted = 1;
            return _entityRepository.Delete(entity);
        }

        public Task<metermodel> Edit(metermodel entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<metermodel> Get(int Key)
        {
            var x =  _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<metermodel> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<metermodel> Remove(metermodel entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<metermodel> FindBy(System.Linq.Expressions.Expression<Func<metermodel, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<metermodel>> RemoveRange(IEnumerable<metermodel> entities)
        {
            throw new NotImplementedException();
        }
    }
}
