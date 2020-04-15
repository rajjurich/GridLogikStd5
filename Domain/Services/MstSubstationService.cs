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
    public interface IMstSubstationService
    {
        Task<mstsubstation> Add(mstsubstation entity);
        Task<IEnumerable<mstsubstation>> AddRange(IEnumerable<mstsubstation> entities);
        Task<mstsubstation> Delete(mstsubstation entity);
        Task<mstsubstation> Edit(mstsubstation entity);
        Task<mstsubstation> Get(int Key);
        IQueryable<mstsubstation> GetAll();
        Task<mstsubstation> Remove(mstsubstation entity);
        IQueryable<mstsubstation> FindBy(Expression<Func<mstsubstation, bool>> predicate);
        Task<IEnumerable<mstsubstation>> RemoveRange(IEnumerable<mstsubstation> entities);
    }
    public class MstSubstationService : IMstSubstationService
    {
        IEntityRepository<mstsubstation> _entityRepository;

        public MstSubstationService(IEntityRepository<mstsubstation> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstsubstation> Add(mstsubstation entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstsubstation>> AddRange(IEnumerable<mstsubstation> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstsubstation> Delete(mstsubstation entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstsubstation> Edit(mstsubstation entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstsubstation> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstsubstation> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<mstsubstation> Remove(mstsubstation entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstsubstation> FindBy(Expression<Func<mstsubstation, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstsubstation>> RemoveRange(IEnumerable<mstsubstation> entities)
        {
            throw new NotImplementedException();
        }
    }
}
