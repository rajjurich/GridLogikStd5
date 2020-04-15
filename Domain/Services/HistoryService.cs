using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GridLogik.ViewModels;
using Domain.Core;
using System.Data.Entity;

namespace Domain.Services
{

    public interface IHistoryService
    {
        Task<loadsurveylog> Add(loadsurveylog entity);
        Task<IEnumerable<loadsurveylog>> AddRange(IEnumerable<loadsurveylog> entities);
        Task<loadsurveylog> Delete(loadsurveylog entity);
        Task<loadsurveylog> Edit(loadsurveylog entity);
        Task<loadsurveylog> Get(int Key);
        IQueryable<loadsurveylog> GetAll();
        Task<loadsurveylog> Remove(loadsurveylog entity);
        IQueryable<loadsurveylog> FindBy(Expression<Func<loadsurveylog, bool>> predicate);
        Task<IEnumerable<loadsurveylog>> RemoveRange(IEnumerable<loadsurveylog> entities);

        dynamic GetMeterByGroupId(long Id);
    }

    public class HistoryService : IHistoryService
    {

        IEntityRepository<loadsurveylog> _entityRepository;
        IUnitOfWork _entity;
        etools_devEntities _db;

        public HistoryService(IEntityRepository<loadsurveylog> entityRepository, IUnitOfWork entity, DbContext db)
        {
            _entityRepository = entityRepository;
            _entity = entity;
            _db = (etools_devEntities)db;
        }

        public Task<loadsurveylog> Add(loadsurveylog entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<loadsurveylog>> AddRange(IEnumerable<loadsurveylog> entities)
        {
            throw new NotImplementedException();
        }

        public Task<loadsurveylog> Delete(loadsurveylog entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<loadsurveylog> Edit(loadsurveylog entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<loadsurveylog> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<loadsurveylog> GetAll()
        {
            var obj = _entityRepository.GetAll().OrderBy(x=>x.meterid).ThenBy(x => x.tstamp);
            return obj;
        }

        public Task<loadsurveylog> Remove(loadsurveylog entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<loadsurveylog> FindBy(Expression<Func<loadsurveylog, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<loadsurveylog>> RemoveRange(IEnumerable<loadsurveylog> entities)
        {
            throw new NotImplementedException();
        }


        public dynamic GetMeterByGroupId(long Id)
        {
            var query = (from mg in _db.metergroups
                         join gp in _db.mstmetergroupdetails on mg.id equals gp.grpid
                         join r in _db.meters on gp.meterid equals r.id
                         where (r.isdeleted == 0 || r.isdeleted == null) && r.isactive == 1 && gp.grpid == Id
                         select new { id = r.id, metername = r.metername }).Distinct().ToList();

           

            return query.ToList();
        }
    }
}
