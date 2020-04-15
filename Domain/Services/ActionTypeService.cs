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
    public interface IActionTypeService
    {
        Task<actiontype> Add(actiontype entity);
        Task<IEnumerable<actiontype>> AddRange(IEnumerable<actiontype> entities);
        Task<actiontype> Delete(actiontype entity);
        Task<actiontype> Edit(actiontype entity);
        Task<actiontype> Get(int Key);
        IQueryable<actiontype> GetAll();
        Task<actiontype> Remove(actiontype entity);
        IQueryable<actiontype> FindBy(Expression<Func<actiontype, bool>> predicate);
        Task<IEnumerable<actiontype>> RemoveRange(IEnumerable<actiontype> entities);
    }
    public class ActionTypeService:IActionTypeService
    {
        

        IEntityRepository<actiontype> _entityRepository;

        public ActionTypeService(IEntityRepository<actiontype> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<actiontype> Add(actiontype entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<actiontype>> AddRange(IEnumerable<actiontype> entities)
        {
            throw new NotImplementedException();
        }

        public Task<actiontype> Delete(actiontype entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<actiontype> Edit(actiontype entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<actiontype> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<actiontype> GetAll()
        {
            //
            //
            //
            //
            //
            //
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<actiontype> Remove(actiontype entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<actiontype> FindBy(Expression<Func<actiontype, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<actiontype>> RemoveRange(IEnumerable<actiontype> entities)
        {
            throw new NotImplementedException();
        }
    }
}
