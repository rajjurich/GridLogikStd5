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
    public interface IMeterGroupService 
    {
        Task<metergroup> Add(metergroup entity);
        Task<IEnumerable<metergroup>> AddRange(IEnumerable<metergroup> entities);
        Task<metergroup> Delete(metergroup entity);
        Task<metergroup> Edit(metergroup entity);
        Task<metergroup> Get(int Key);
        IQueryable<metergroup> GetAll();
        Task<metergroup> Remove(metergroup entity);
        IQueryable<metergroup> FindBy(Expression<Func<metergroup, bool>> predicate);
        Task<IEnumerable<metergroup>> RemoveRange(IEnumerable<metergroup> entities);
    }
    public class MeterGroupService : IMeterGroupService
    {
        IEntityRepository<metergroup> entityRepository;
        public MeterGroupService(IEntityRepository<metergroup> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<metergroup> Add(metergroup entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<metergroup>> AddRange(IEnumerable<metergroup> entities)
        {
            return entityRepository.AddRange(entities);
        }

        public Task<metergroup> Delete(metergroup entity)
        {
            entity.isdeleted = 1;
            return entityRepository.Delete(entity);
        }

        public Task<metergroup> Edit(metergroup entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<metergroup> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<metergroup> GetAll()
        {
            return entityRepository.GetAll().Where(x=>x.isdeleted==0 || x.isdeleted == null);
        }

        public Task<metergroup> Remove(metergroup entity)
        {
            return entityRepository.Remove(entity);
        }

        public IQueryable<metergroup> FindBy(Expression<Func<metergroup, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<metergroup>> RemoveRange(IEnumerable<metergroup> entities)
        {
            return entityRepository.RemoveRange(entities);
        }
    }
}
