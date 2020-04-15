using Domain.Core;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IEmployeeService
    {
        Task<mstemployee> Add(mstemployee entity);
        Task<IEnumerable<mstemployee>> AddRange(IEnumerable<mstemployee> entities);
        Task<mstemployee> Delete(mstemployee entity);
        Task<mstemployee> Edit(mstemployee entity);
        Task<mstemployee> Get(int Key);
        IQueryable<mstemployee> GetAll();
        Task<mstemployee> Remove(mstemployee entity);
        IQueryable<mstemployee> FindBy(Expression<Func<mstemployee, bool>> predicate);
        Task<IEnumerable<mstemployee>> RemoveRange(IEnumerable<mstemployee> entities);

        IQueryable<mstemployee> GetUnusedEmployees();
    }
    public class EmployeeService : IEmployeeService
    {
        IEntityRepository<mstemployee> _entityRepository;
        IUnitOfWork _unitOfWork;
        etools_devEntities _db;

        public EmployeeService(IEntityRepository<mstemployee> entityRepository, IUnitOfWork unitOfWork, DbContext db)
        {
            _entityRepository = entityRepository;
            _unitOfWork = unitOfWork;
            _db = (etools_devEntities)db;
        }

        public Task<mstemployee> Add(mstemployee entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstemployee>> AddRange(IEnumerable<mstemployee> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstemployee> Delete(mstemployee entity)
        {
            entity.empisdeleted = 1;
            return _entityRepository.Delete(entity);
        }

        public Task<mstemployee> Edit(mstemployee entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstemployee> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<mstemployee> GetAll()
        {
            return _entityRepository.GetAll();
        }

        public Task<mstemployee> Remove(mstemployee entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstemployee> FindBy(Expression<Func<mstemployee, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstemployee>> RemoveRange(IEnumerable<mstemployee> entities)
        {
            throw new NotImplementedException();
        }


        public IQueryable<mstemployee> GetUnusedEmployees()
        {
            var mstemployees = _db.mstemployees.Where(e => !_db.mstusers.Any(u => u.usremployeeid == e.empid) && e.empisdeleted == 0 || e.empisdeleted == null)
                .Select(e => e);

            return mstemployees;

        }
    }
}
