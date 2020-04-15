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
    public interface IRealtimeAlarmService
    {
        Task<rtalarm> Add(rtalarm entity);
        Task<IEnumerable<rtalarm>> AddRange(IEnumerable<rtalarm> entities);
        Task<rtalarm> Delete(rtalarm entity);
        Task<rtalarm> Edit(rtalarm entity);
        Task<rtalarm> Get(int Key);
        IQueryable<rtalarm> GetAll();
        Task<rtalarm> Remove(rtalarm entity);
        IQueryable<rtalarm> FindBy(Expression<Func<rtalarm, bool>> predicate);
        Task<IEnumerable<rtalarm>> RemoveRange(IEnumerable<rtalarm> entities);
    }
    public class RealtimeAlarmService : IRealtimeAlarmService
    {        

        IEntityRepository<rtalarm> _entityRepository;

        public RealtimeAlarmService(IEntityRepository<rtalarm> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<rtalarm> Add(rtalarm entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<rtalarm>> AddRange(IEnumerable<rtalarm> entities)
        {
            throw new NotImplementedException();
        }

        public Task<rtalarm> Delete(rtalarm entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<rtalarm> Edit(rtalarm entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<rtalarm> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<rtalarm> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<rtalarm> Remove(rtalarm entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<rtalarm> FindBy(Expression<Func<rtalarm, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<rtalarm>> RemoveRange(IEnumerable<rtalarm> entities)
        {
            throw new NotImplementedException();
        }
    }
}
