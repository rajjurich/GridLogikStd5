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
    public interface IConsumerService
    {
        Task<consumer> Add(consumer entity);
        Task<IEnumerable<consumer>> AddRange(IEnumerable<consumer> entities);
        Task<consumer> Delete(consumer entity);
        Task<consumer> Edit(consumer entity);
        Task<consumer> Get(int Key);
        IQueryable<consumer> GetAll();
        Task<consumer> Remove(consumer entity);
        IQueryable<consumer> FindBy(Expression<Func<consumer, bool>> predicate);
        Task<IEnumerable<consumer>> RemoveRange(IEnumerable<consumer> entities);
    }
    public class ConsumerService : IConsumerService
    {
        IEntityRepository<consumer> entityRepository;
        public ConsumerService(IEntityRepository<consumer> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<consumer> Add(consumer entity)
        {            
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<consumer>> AddRange(IEnumerable<consumer> entities)
        {
            throw new NotImplementedException();
        }

        public Task<consumer> Delete(consumer entity)
        {
            entity.isdeleted = 1;
            return entityRepository.Delete(entity);
        }

        public Task<consumer> Edit(consumer entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<consumer> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<consumer> GetAll()
        {
            return entityRepository.GetAll().Where(x => x.isdeleted != 1);
        }

        public Task<consumer> Remove(consumer entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<consumer> FindBy(Expression<Func<consumer, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<consumer>> RemoveRange(IEnumerable<consumer> entities)
        {
            throw new NotImplementedException();
        }
    }
}
