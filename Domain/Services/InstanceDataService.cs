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
    public interface IInstanceDataService
    {
        Task<instancedata> Add(instancedata entity);
        Task<IEnumerable<instancedata>> AddRange(IEnumerable<instancedata> entities);
        Task<instancedata> Delete(instancedata entity);
        Task<instancedata> Edit(instancedata entity);
        Task<instancedata> Get(int Key);
        IQueryable<instancedata> GetAll();
        Task<instancedata> Remove(instancedata entity);
        IQueryable<instancedata> FindBy(Expression<Func<instancedata, bool>> predicate);
        Task<IEnumerable<instancedata>> RemoveRange(IEnumerable<instancedata> entities);
        IQueryable<instancedata> GetInstanceDataByMeterIds(List<long> meterids);
        IQueryable<instancedata> GetInstanceDynamicDataByMeterIdsForSpecificPeriod(
            string tablename, string columnname, List<long> meterids, string fromDatetime, string toDateTime);
    }

    public class InstanceDataService : IInstanceDataService
    {
        IEntityRepository<instancedata> entityRepository;
        public InstanceDataService(IEntityRepository<instancedata> entityRepository)
        {
            this.entityRepository = entityRepository;
        }
        public Task<instancedata> Add(instancedata entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<instancedata>> AddRange(IEnumerable<instancedata> entities)
        {
            throw new NotImplementedException();
        }

        public Task<instancedata> Delete(instancedata entity)
        {
            throw new NotImplementedException();
        }

        public Task<instancedata> Edit(instancedata entity)
        {
            throw new NotImplementedException();
        }

        public Task<instancedata> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<instancedata> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<instancedata> Remove(instancedata entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<instancedata> FindBy(Expression<Func<instancedata, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<instancedata>> RemoveRange(IEnumerable<instancedata> entities)
        {
            throw new NotImplementedException();
        }


        public IQueryable<instancedata> GetInstanceDataByMeterIds(List<long> meterids)
        {
            return entityRepository.FindBy(x => meterids.Contains(x.id));
        }


        public IQueryable<instancedata> GetInstanceDynamicDataByMeterIdsForSpecificPeriod(
            string tablename, string columnname, List<long> meterids, string fromDatetime, string toDateTime)
        {
            if (fromDatetime != "" && fromDatetime != null
                    && toDateTime != "" && toDateTime != null)
            {
                DateTime dtFrom = Convert.ToDateTime(fromDatetime);
                DateTime dtTo = Convert.ToDateTime(toDateTime);

                return entityRepository.FindBy(x => meterids.Contains(x.id) && x.tstamp > dtFrom && x.tstamp <= dtTo);
            }

            return entityRepository.FindBy(x => meterids.Contains(x.id));
        }
    }
}
