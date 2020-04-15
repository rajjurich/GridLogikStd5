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
    public interface IParameterMFService
    {
        Task<parametermf> Add(parametermf entity);
        Task<IEnumerable<parametermf>> AddRange(IEnumerable<parametermf> entities);
        Task<parametermf> Delete(parametermf entity);
        Task<parametermf> Edit(parametermf entity);
        Task<parametermf> Get(int Key);
        IQueryable<parametermf> GetAll();
        Task<parametermf> Remove(parametermf entity);
        IQueryable<parametermf> FindBy(Expression<Func<parametermf, bool>> predicate);
        Task<IEnumerable<parametermf>> RemoveRange(IEnumerable<parametermf> entities);

        IQueryable<parametermf> GetParameterMFWithValuesByMeterId(long id);
    }
    public class ParameterMFService : IParameterMFService
    {
        public IEntityRepository<parametermf> _entityRepository { get; set; }

        public ParameterMFService(IEntityRepository<parametermf> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public Task<parametermf> Add(parametermf entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<parametermf>> AddRange(IEnumerable<parametermf> entities)
        {
            throw new NotImplementedException();
        }

        public Task<parametermf> Delete(parametermf entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<parametermf> Edit(parametermf entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<parametermf> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<parametermf> GetAll()
        {
            return _entityRepository.GetAll();
        }

        public Task<parametermf> Remove(parametermf entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<parametermf> FindBy(Expression<Func<parametermf, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<parametermf>> RemoveRange(IEnumerable<parametermf> entities)
        {
            throw new NotImplementedException();
        }



        public IQueryable<parametermf> GetParameterMFWithValuesByMeterId(long id)
        {
            var parametermfs = _entityRepository.GetAll().Where(x => x.meterid == id);                
                

            //if (parametermfs.Count == 0)
            //{
            //    List<prmglobal> prmglobals = (from p in _db.prmglobals
            //                                  where p.prmunit == "Scaling"
            //                                  select p).ToList();

            //    foreach (var item in prmglobals)
            //    {
            //        parametermf pmf = new parametermf();
            //        pmf.grouptype = item.prmmodule;
            //        pmf.multiplicationfactor = 0;
            //        parametermfs.Add(pmf);
            //    }

            //}
            //else
            //{
            //    List<prmglobal> prmglobals = (from p in _db.prmglobals
            //                                  where p.prmunit == "Scaling"
            //                                  select p).ToList();

            //    var result = prmglobals.Where(p => !parametermfs.Any(p2 => p2.grouptype == p.prmmodule));
            //    foreach (var item in result)
            //    {
            //        parametermf pmf = new parametermf();
            //        pmf.grouptype = item.prmmodule;
            //        pmf.multiplicationfactor = 0;
            //        parametermfs.Add(pmf);
            //    }

            //}
            return parametermfs.AsQueryable();

        }
    }
}
