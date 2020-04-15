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
    public interface IStandardAlaramService
    {
        Task<standardalarm> Add(standardalarm entity);
        Task<IEnumerable<standardalarm>> AddRange(IEnumerable<standardalarm> entities);
        Task<standardalarm> Delete(standardalarm entity);
        Task<standardalarm> Edit(standardalarm entity);
        Task<standardalarm> Get(int Key);
        IQueryable<standardalarm> GetAll();
        Task<standardalarm> Remove(standardalarm entity);
        IQueryable<standardalarm> FindBy(Expression<Func<standardalarm, bool>> predicate);
        Task<IEnumerable<standardalarm>> RemoveRange(IEnumerable<standardalarm> entities);
    }
    public class StandardAlaramService : IStandardAlaramService
    {        

        IEntityRepository<standardalarm> _entityRepository;

        public StandardAlaramService(IEntityRepository<standardalarm> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<standardalarm> Add(standardalarm entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<standardalarm>> AddRange(IEnumerable<standardalarm> entities)
        {
            throw new NotImplementedException();
        }

        public Task<standardalarm> Delete(standardalarm entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<standardalarm> Edit(standardalarm entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<standardalarm> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<standardalarm> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<standardalarm> Remove(standardalarm entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<standardalarm> FindBy(Expression<Func<standardalarm, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<standardalarm>> RemoveRange(IEnumerable<standardalarm> entities)
        {
            throw new NotImplementedException();
        }
    }
}
