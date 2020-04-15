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
    public interface IDCSGFuelStagedService
    {
        Task<dcsg> Add(dcsg entity);
        Task<IEnumerable<dcsg>> AddRange(IEnumerable<dcsg> entities);
        Task<dcsg> Delete(dcsg entity);
        Task<dcsg> Edit(dcsg entity);
        Task<dcsg> Get(int Key);
        IQueryable<dcsg> GetAll();

        IQueryable<dcsg> GetAllDistinct();
        Task<dcsg> Remove(dcsg entity);
        IQueryable<dcsg> FindBy(Expression<Func<dcsg, bool>> predicate);
        Task<IEnumerable<dcsg>> RemoveRange(IEnumerable<dcsg> entities);
    }
    public class DCSGFuelStagedService : IDCSGFuelStagedService
    {        

        IEntityRepository<dcsg> _entityRepository;

        public DCSGFuelStagedService(IEntityRepository<dcsg> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<dcsg> Add(dcsg entity)
        {
            entity.upload_date = DateTime.Now;
            entity.timestampid = GetBlockInformation.getTimeStampId(entity.tstamp);
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<dcsg>> AddRange(IEnumerable<dcsg> entities)
        {
            throw new NotImplementedException();
        }

        public Task<dcsg> Delete(dcsg entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<dcsg> Edit(dcsg entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<dcsg> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<dcsg> GetAll()
        {
            var obj = _entityRepository.GetAll();
            return obj;
        }

        public IQueryable<dcsg> GetAllDistinct()
        {
            var obj = _entityRepository.GetAll().GroupBy(x => x.revision).Select(x=>x.FirstOrDefault());
            return obj;
        }

        public Task<dcsg> Remove(dcsg entity)
        {
            return _entityRepository.Remove(entity);
        }

        public IQueryable<dcsg> FindBy(Expression<Func<dcsg, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<dcsg>> RemoveRange(IEnumerable<dcsg> entities)
        {
            throw new NotImplementedException();
        }
    }
}
