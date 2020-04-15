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
    public interface IConsumerCategoryService
    {
        Task<consumercategory> Add(consumercategory entity);
        Task<IEnumerable<consumercategory>> AddRange(IEnumerable<consumercategory> entities);
        Task<consumercategory> Delete(consumercategory entity);
        Task<consumercategory> Edit(consumercategory entity);
        Task<consumercategory> Get(int Key);
        IQueryable<consumercategory> GetAll();
        Task<consumercategory> Remove(consumercategory entity);
        IQueryable<consumercategory> FindBy(Expression<Func<consumercategory, bool>> predicate);
        Task<IEnumerable<consumercategory>> RemoveRange(IEnumerable<consumercategory> entities);
    }
    public class ConsumerCategoryService : IConsumerCategoryService
    {
        IEntityRepository<consumercategory> entityRepository;
        public ConsumerCategoryService(IEntityRepository<consumercategory> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<consumercategory> Add(consumercategory entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<consumercategory>> AddRange(IEnumerable<consumercategory> entities)
        {
            throw new NotImplementedException();
        }

        public Task<consumercategory> Delete(consumercategory entity)
        {
            entity.categoryisdeleted = 1;
            return entityRepository.Delete(entity);
        }

        public Task<consumercategory> Edit(consumercategory entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<consumercategory> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<consumercategory> GetAll()
        {
            return entityRepository.GetAll().Where(x => x.categoryisdeleted != 1);
        }

        public Task<consumercategory> Remove(consumercategory entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<consumercategory> FindBy(Expression<Func<consumercategory, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<consumercategory>> RemoveRange(IEnumerable<consumercategory> entities)
        {
            throw new NotImplementedException();
        }
    }
}
