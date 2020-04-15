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
    public interface IMstzoneService
    {
        Task<mstzone> Add(mstzone entity);
        Task<IEnumerable<mstzone>> AddRange(IEnumerable<mstzone> entities);
        Task<mstzone> Delete(mstzone entity);
        Task<mstzone> Edit(mstzone entity);
        Task<mstzone> Get(int Key);
        IQueryable<mstzone> GetAll();
        Task<mstzone> Remove(mstzone entity);
        IQueryable<mstzone> FindBy(Expression<Func<mstzone, bool>> predicate);
        Task<IEnumerable<mstzone>> RemoveRange(IEnumerable<mstzone> entities);
    }
    public class MstzoneService : IMstzoneService
    {
        IEntityRepository<mstzone> _entityRepository;

        public MstzoneService(IEntityRepository<mstzone> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<mstzone> Add(mstzone entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstzone>> AddRange(IEnumerable<mstzone> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstzone> Delete(mstzone entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstzone> Edit(mstzone entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstzone> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstzone> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<mstzone> Remove(mstzone entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstzone> FindBy(Expression<Func<mstzone, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstzone>> RemoveRange(IEnumerable<mstzone> entities)
        {
            throw new NotImplementedException();
        }
    }
}
