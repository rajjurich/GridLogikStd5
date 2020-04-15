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
    public interface ICommunicationDetailLinkService
    {
        Task<communicationdetaillink> Add(communicationdetaillink entity);
        Task<IEnumerable<communicationdetaillink>> AddRange(IEnumerable<communicationdetaillink> entities);
        Task<communicationdetaillink> Delete(communicationdetaillink entity);
        Task<communicationdetaillink> Edit(communicationdetaillink entity);
        Task<communicationdetaillink> Get(int Key);
        IQueryable<communicationdetaillink> GetAll();
        Task<communicationdetaillink> Remove(communicationdetaillink entity);
        IQueryable<communicationdetaillink> FindBy(Expression<Func<communicationdetaillink, bool>> predicate);
        Task<IEnumerable<communicationdetaillink>> RemoveRange(IEnumerable<communicationdetaillink> entities);
    }
    public class CommunicationDetailLinkService : ICommunicationDetailLinkService
    {
          IEntityRepository<communicationdetaillink> _entityRepository;
          public CommunicationDetailLinkService(IEntityRepository<communicationdetaillink> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public Task<communicationdetaillink> Add(communicationdetaillink entity)
        {            
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<communicationdetaillink>> AddRange(IEnumerable<communicationdetaillink> entities)
        {
            throw new NotImplementedException();
        }

        public Task<communicationdetaillink> Delete(communicationdetaillink entity)
        {
            
            return _entityRepository.Delete(entity);
        }

        public Task<communicationdetaillink> Edit(communicationdetaillink entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<communicationdetaillink> Get(int Key)
        {
            var x =  _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<communicationdetaillink> GetAll()
        {
            var obj = _entityRepository.GetAll();
            return obj;
        }

        public Task<communicationdetaillink> Remove(communicationdetaillink entity)
        {
            return _entityRepository.Remove(entity);
        }

        public IQueryable<communicationdetaillink> FindBy(System.Linq.Expressions.Expression<Func<communicationdetaillink, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<communicationdetaillink>> RemoveRange(IEnumerable<communicationdetaillink> entities)
        {
            throw new NotImplementedException();
        }

    }
}
