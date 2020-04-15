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
    public interface IReportManuallyService
    {
        Task<manualreport> Add(manualreport entity);
        Task<IEnumerable<manualreport>> AddRange(IEnumerable<manualreport> entities);
        Task<manualreport> Delete(manualreport entity);
        Task<manualreport> Edit(manualreport entity);
        Task<manualreport> Get(int Key);
        IQueryable<manualreport> GetAll();
        Task<manualreport> Remove(manualreport entity);
        IQueryable<manualreport> FindBy(Expression<Func<manualreport, bool>> predicate);
        Task<IEnumerable<manualreport>> RemoveRange(IEnumerable<manualreport> entities);
    }
    public class ReportManuallyService : IReportManuallyService
    {
        IEntityRepository<manualreport> _entityRepository;
        

        public ReportManuallyService(IEntityRepository<manualreport> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<manualreport> Add(manualreport entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<manualreport>> AddRange(IEnumerable<manualreport> entities)
        {
            throw new NotImplementedException();
        }

        public Task<manualreport> Delete(manualreport entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<manualreport> Edit(manualreport entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<manualreport> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<manualreport> GetAll()
        {
            var obj = _entityRepository.GetAll();
            return obj;
        }

        public Task<manualreport> Remove(manualreport entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<manualreport> FindBy(Expression<Func<manualreport, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<manualreport>> RemoveRange(IEnumerable<manualreport> entities)
        {
            throw new NotImplementedException();
        }

    }
}
