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
    public interface IMstRegionService
    {
        Task<mstregion> Add(mstregion entity);
        Task<IEnumerable<mstregion>> AddRange(IEnumerable<mstregion> entities);
        Task<mstregion> Delete(mstregion entity);
        Task<mstregion> Edit(mstregion entity);
        Task<mstregion> Get(int Key);
        IQueryable<mstregion> GetAll();
        Task<mstregion> Remove(mstregion entity);
        IQueryable<mstregion> FindBy(Expression<Func<mstregion, bool>> predicate);
        Task<IEnumerable<mstregion>> RemoveRange(IEnumerable<mstregion> entities);
    }
    public class MstRegionService : IMstRegionService
    {
        IEntityRepository<mstregion> _entityRepository;

        public MstRegionService(IEntityRepository<mstregion> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstregion> Add(mstregion entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstregion>> AddRange(IEnumerable<mstregion> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstregion> Delete(mstregion entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstregion> Edit(mstregion entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstregion> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstregion> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<mstregion> Remove(mstregion entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstregion> FindBy(Expression<Func<mstregion, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstregion>> RemoveRange(IEnumerable<mstregion> entities)
        {
            throw new NotImplementedException();
        }
    }
}
