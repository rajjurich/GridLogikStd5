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
    public interface ImstutilityService
    {
        Task<mstutility> Add(mstutility entity);
        Task<IEnumerable<mstutility>> AddRange(IEnumerable<mstutility> entities);
        Task<mstutility> Delete(mstutility entity);
        Task<mstutility> Edit(mstutility entity);
        Task<mstutility> Get(int Key);
        IQueryable<mstutility> GetAll();
        Task<mstutility> Remove(mstutility entity);
        IQueryable<mstutility> FindBy(Expression<Func<mstutility, bool>> predicate);
        Task<IEnumerable<mstutility>> RemoveRange(IEnumerable<mstutility> entities);
    }

    public class mstutilityService : ImstutilityService
    {
        IEntityRepository<mstutility> _entityRepository;

        public mstutilityService(IEntityRepository<mstutility> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstutility> Add(mstutility entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstutility>> AddRange(IEnumerable<mstutility> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstutility> Delete(mstutility entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstutility> Edit(mstutility entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstutility> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstutility> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<mstutility> Remove(mstutility entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstutility> FindBy(Expression<Func<mstutility, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstutility>> RemoveRange(IEnumerable<mstutility> entities)
        {
            throw new NotImplementedException();
        }
    }
}
