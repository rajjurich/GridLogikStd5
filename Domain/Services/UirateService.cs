using Domain.Common;
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

    public interface IUirateService
    {
        Task<uirate> Add(uirate entity);
        Task<IEnumerable<uirate>> AddRange(IEnumerable<uirate> entities);
        Task<uirate> Delete(uirate entity);
        Task<uirate> Edit(uirate entity);
        Task<uirate> Get(int Key);
        IQueryable<uirate> GetAll();

        IQueryable<uirate> GetAllDistinct();
        Task<uirate> Remove(uirate entity);
        IQueryable<uirate> FindBy(Expression<Func<uirate, bool>> predicate);
        Task<IEnumerable<uirate>> RemoveRange(IEnumerable<uirate> entities);
    }
    public class UirateService : IUirateService
    {
        IEntityRepository<uirate> _entityRepository;

        public UirateService(IEntityRepository<uirate> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<uirate> Add(uirate entity)
        {
            entity.upload_date = DateTime.Now;
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<uirate>> AddRange(IEnumerable<uirate> entities)
        {
            throw new NotImplementedException();
        }

        public Task<uirate> Delete(uirate entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<uirate> Edit(uirate entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<uirate> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<uirate> GetAll()
        {
            var obj = _entityRepository.GetAll();
            return obj;
        }

        public IQueryable<uirate> GetAllDistinct()
        {
            var obj = _entityRepository.GetAll().GroupBy(x => x.revision).Select(x => x.FirstOrDefault());
            return obj;
        }

        public Task<uirate> Remove(uirate entity)
        {
            return _entityRepository.Remove(entity);
        }

        public IQueryable<uirate> FindBy(Expression<Func<uirate, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<uirate>> RemoveRange(IEnumerable<uirate> entities)
        {
            throw new NotImplementedException();
        }
    }
}
