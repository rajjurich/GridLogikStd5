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
    public interface IMstDivisionService
    {
        Task<mstdivision> Add(mstdivision entity);
        Task<IEnumerable<mstdivision>> AddRange(IEnumerable<mstdivision> entities);
        Task<mstdivision> Delete(mstdivision entity);
        Task<mstdivision> Edit(mstdivision entity);
        Task<mstdivision> Get(int Key);
        IQueryable<mstdivision> GetAll();
        Task<mstdivision> Remove(mstdivision entity);
        IQueryable<mstdivision> FindBy(Expression<Func<mstdivision, bool>> predicate);
        Task<IEnumerable<mstdivision>> RemoveRange(IEnumerable<mstdivision> entities);
    }
    public class MstDivisionService : IMstDivisionService
    {
        IEntityRepository<mstdivision> _entityRepository;

        public MstDivisionService(IEntityRepository<mstdivision> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstdivision> Add(mstdivision entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstdivision>> AddRange(IEnumerable<mstdivision> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstdivision> Delete(mstdivision entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstdivision> Edit(mstdivision entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstdivision> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstdivision> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<mstdivision> Remove(mstdivision entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstdivision> FindBy(Expression<Func<mstdivision, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstdivision>> RemoveRange(IEnumerable<mstdivision> entities)
        {
            throw new NotImplementedException();
        }
    }
}
