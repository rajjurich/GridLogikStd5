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
    public interface IMeterGroupDetailService
    {
        Task<mstmetergroupdetail> Add(mstmetergroupdetail entity);
        Task<IEnumerable<mstmetergroupdetail>> AddRange(IEnumerable<mstmetergroupdetail> entities);
        Task<mstmetergroupdetail> Delete(mstmetergroupdetail entity);
        Task<mstmetergroupdetail> Edit(mstmetergroupdetail entity);
        Task<mstmetergroupdetail> Get(int Key);
        IQueryable<mstmetergroupdetail> GetAll();
        Task<mstmetergroupdetail> Remove(mstmetergroupdetail entity);
        IQueryable<mstmetergroupdetail> FindBy(Expression<Func<mstmetergroupdetail, bool>> predicate);
        Task<IEnumerable<mstmetergroupdetail>> RemoveRange(IEnumerable<mstmetergroupdetail> entities);

        IQueryable<mstmetergroupdetail> GetGroupConfigByGroupId(int id);
    }
    public class MeterGroupDetailService : IMeterGroupDetailService
    {
        IEntityRepository<mstmetergroupdetail> entityRepository;
        public MeterGroupDetailService(IEntityRepository<mstmetergroupdetail> entityRepository)
        {
            this.entityRepository = entityRepository;

        }

        public Task<mstmetergroupdetail> Add(mstmetergroupdetail entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstmetergroupdetail>> AddRange(IEnumerable<mstmetergroupdetail> entities)
        {
            return entityRepository.AddRange(entities);
        }

        public Task<mstmetergroupdetail> Delete(mstmetergroupdetail entity)
        {            
            return entityRepository.Delete(entity);
        }

        public Task<mstmetergroupdetail> Edit(mstmetergroupdetail entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<mstmetergroupdetail> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<mstmetergroupdetail> GetAll()
        {
            return entityRepository.GetAll();
        }

        public Task<mstmetergroupdetail> Remove(mstmetergroupdetail entity)
        {
            return entityRepository.Remove(entity);
        }

        public IQueryable<mstmetergroupdetail> FindBy(Expression<Func<mstmetergroupdetail, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstmetergroupdetail>> RemoveRange(IEnumerable<mstmetergroupdetail> entities)
        {
            return entityRepository.RemoveRange(entities);
        }


        public IQueryable<mstmetergroupdetail> GetGroupConfigByGroupId(int id)
        {
            return entityRepository.FindBy(x => x.grpid == id);
        }
    }
}
