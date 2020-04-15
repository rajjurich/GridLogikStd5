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
    public interface IExceptionService
    {
        Task<msterrorlog> Add(msterrorlog entity);
        Task<IEnumerable<msterrorlog>> AddRange(IEnumerable<msterrorlog> entities);
        Task<msterrorlog> Delete(msterrorlog entity);
        Task<msterrorlog> Edit(msterrorlog entity);
        Task<msterrorlog> Get(int Key);
        IQueryable<msterrorlog> GetAll();
        Task<msterrorlog> Remove(msterrorlog entity);
        IQueryable<msterrorlog> FindBy(Expression<Func<msterrorlog, bool>> predicate);
        Task<IEnumerable<msterrorlog>> RemoveRange(IEnumerable<msterrorlog> entities);
    }
    public class ExceptionService : IExceptionService
    {
        IEntityRepository<msterrorlog> _entityRepository;
        public ExceptionService(IEntityRepository<msterrorlog> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<msterrorlog> Add(msterrorlog entity)
        {
            await _entityRepository.Add(entity);
            return entity;
        }

        public Task<IEnumerable<msterrorlog>> AddRange(IEnumerable<msterrorlog> entities)
        {
            throw new NotImplementedException();
        }

        public Task<msterrorlog> Delete(msterrorlog entity)
        {
            throw new NotImplementedException();
        }

        public Task<msterrorlog> Edit(msterrorlog entity)
        {
            throw new NotImplementedException();
        }

        public Task<msterrorlog> Get(int Key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<msterrorlog> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<msterrorlog> Remove(msterrorlog entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<msterrorlog> FindBy(Expression<Func<msterrorlog, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<msterrorlog>> RemoveRange(IEnumerable<msterrorlog> entities)
        {
            throw new NotImplementedException();
        }
    }
}
