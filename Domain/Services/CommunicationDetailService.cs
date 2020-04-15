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
    public interface ICommunicationDetailService
    {
        Task<communicationdetail> Add(communicationdetail entity);
        Task<IEnumerable<communicationdetail>> AddRange(IEnumerable<communicationdetail> entities);
        Task<communicationdetail> Delete(communicationdetail entity);
        Task<communicationdetail> Edit(communicationdetail entity);
        Task<communicationdetail> Get(int Key);
        IQueryable<communicationdetail> GetAll();
        Task<communicationdetail> Remove(communicationdetail entity);
        IQueryable<communicationdetail> FindBy(Expression<Func<communicationdetail, bool>> predicate);
        Task<IEnumerable<communicationdetail>> RemoveRange(IEnumerable<communicationdetail> entities);
        IQueryable<communicationdetail> GetUnlinked(IEnumerable<long?> linkedConvertors, List<long> communicationtypeids);        
    }
    public class CommunicationDetailService : ICommunicationDetailService
    {
        IEntityRepository<communicationdetail> _entityRepository;
        public CommunicationDetailService(IEntityRepository<communicationdetail> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public Task<communicationdetail> Add(communicationdetail entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<communicationdetail>> AddRange(IEnumerable<communicationdetail> entities)
        {
            throw new NotImplementedException();
        }

        public Task<communicationdetail> Delete(communicationdetail entity)
        {
            entity.isdeleted = 1;
            return _entityRepository.Delete(entity);
        }

        public Task<communicationdetail> Edit(communicationdetail entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<communicationdetail> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<communicationdetail> GetAll()
        {
            var obj = _entityRepository.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
            return obj;
        }

        public Task<communicationdetail> Remove(communicationdetail entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<communicationdetail> FindBy(System.Linq.Expressions.Expression<Func<communicationdetail, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<communicationdetail>> RemoveRange(IEnumerable<communicationdetail> entities)
        {
            throw new NotImplementedException();
        }


        public IQueryable<communicationdetail> GetUnlinked(IEnumerable<long?> linkedConvertors, List<long> communicationtypeids)
        {
            var obj = _entityRepository.GetAll().Where(x => (x.isdeleted == 0 || x.isdeleted == null) &&  communicationtypeids.Contains(x.communicationtypeid) && !linkedConvertors.Contains(x.id));
            return obj;
        }

    }
}
