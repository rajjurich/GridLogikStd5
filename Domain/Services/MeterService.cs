using Domain.Core;
using Domain.Entities;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IMeterService
    {
        Task<meter> Add(meter entity);
        Task<IEnumerable<meter>> AddRange(IEnumerable<meter> entities);
        Task<meter> Delete(meter entity);
        Task<meter> Edit(meter entity);
        Task<meter> Get(int Key);
        IQueryable<meter> GetAll();

        
        Task<meter> Remove(meter entity);
        IQueryable<meter> FindBy(Expression<Func<meter, bool>> predicate);
        Task<IEnumerable<meter>> RemoveRange(IEnumerable<meter> entities);

        IQueryable<meter> GetMeterDetails(long Id);
        IQueryable<meter> GetUnlinked(IEnumerable<long?> linkedMeters);

        List<MeterVM> GetMetersByMultipleGroupID(string id);
        List<MeterVM> GetMetersByGroupID(string id);
        
    }
    public class MeterService : IMeterService
    {
        IEntityRepository<meter> _entityRepository;
        etools_devEntities _db;
        public MeterService(IEntityRepository<meter> entityRepository, DbContext db)
        {
            _entityRepository = entityRepository;
            _db = (etools_devEntities)db;
        }

        public async Task<meter> Add(meter entity)
        {
            await _entityRepository.Add(entity);
            return entity;
        }

        public Task<IEnumerable<meter>> AddRange(IEnumerable<meter> entities)
        {
            throw new NotImplementedException();
        }

        public Task<meter> Delete(meter entity)
        {
            entity.isdeleted = 1;
            return _entityRepository.Delete(entity);
        }

        public Task<meter> Edit(meter entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<meter> Get(int Key)
        {
            return _entityRepository.Get(Key);
        }

        public IQueryable<meter> GetAll()
        {
            return _entityRepository.GetAll();
        }


        


        public Task<meter> Remove(meter entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<meter> FindBy(System.Linq.Expressions.Expression<Func<meter, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<meter>> RemoveRange(IEnumerable<meter> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<meter> GetMeterDetails(long Id)
        {
            var meters = (from mtr in _db.meters
                          join model in _db.metermodels
                          on mtr.modelid equals model.id
                          join mt in _db.metertypes
                          on mtr.metertypeid equals mt.id
                          where (mtr.isdeleted == null || mtr.isdeleted == 0)
                          && (model.isdeleted == null || model.isdeleted == 0)
                          && (mt.isdeleted == null || mt.isdeleted == 0)
                          && mtr.isactive == 1
                          select new
                          {
                              metername = mtr.metername,
                              metermodel = model,
                              //communicationtype = "",
                              ctprimary = mtr.ctprimary,
                              ctsecondary = mtr.ctsecondary,
                              meterno = mtr.meterno,
                              id = mtr.id,
                              metertype = mt,
                              serialno = mtr.serialno,
                              //modbusid = 0,
                              location = mtr.location,
                              rolloverlimit = mtr.rolloverlimit,
                              parametermfs = (
                              from pms in _db.parametermfs.OrderBy(m => m.grouptype)
                              where pms.meterid == mtr.id
                              select new
                              {
                                  multiplicationfactor = pms.multiplicationfactor
                              }).ToList()
                          }).ToList()
                          .Select(x => new meter
                          {
                              metername = x.metername,
                              metermodel = x.metermodel,
                              //communicationtype = "",
                              ctprimary = x.ctprimary,
                              ctsecondary = x.ctsecondary,
                              meterno = x.meterno,
                              id = x.id,
                              metertype = x.metertype,
                              serialno = x.serialno,
                              //modbusid = 0,
                              location = x.location,
                              rolloverlimit = x.rolloverlimit,
                              //parametermfs = x.parametermfs.ToList()
                          })
                          .AsQueryable();


            return meters;
        }


        public IQueryable<meter> GetUnlinked(IEnumerable<long?> linkedMeters)
        {
            var obj = _entityRepository.GetAll().Where(x => (x.isdeleted == 0 || x.isdeleted == null) && !linkedMeters.Contains(x.id));
            return obj;
        }


        public List<MeterVM> GetMetersByMultipleGroupID(string id)
        {
            List<long?> TagIds = id.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            List<MeterVM> lstmeter = new List<MeterVM>();
            var query = (from m in _db.meters join g in _db.mstmetergroupdetails on m.id equals g.meterid where TagIds.Contains(g.grpid) && (m.isdeleted == 0 || m.isdeleted == null) select new { m.id, m.metername }).Distinct().ToList();

            foreach (dynamic obj in query)
            {
                MeterVM mtr = new MeterVM();
                mtr.ID = obj.id;
                mtr.MeterName = obj.metername;
                lstmeter.Add(mtr);
            }
            return lstmeter;
        }


        public List<MeterVM> GetMetersByGroupID(string id)
        {
            List<long?> TagIds = id.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();
            List<MeterVM> lstmeter = new List<MeterVM>();
            //var query = (from m in db.meters join g in db.groupconfigurations on m.id equals g.meterid where g.groupid == id && (m.isdeleted == 0 || m.isdeleted == null) && (g.isdeleted == 0 || g.isdeleted == null) select new { m.id, m.metername }).Distinct().ToList();
            var query = (from m in _db.meters join g in _db.mstmetergroupdetails on m.id equals g.meterid where TagIds.Contains(g.grpid) && (m.isdeleted == 0 || m.isdeleted == null) select new { m.id, m.metername }).Distinct().ToList();

            foreach (dynamic obj in query)
            {
                MeterVM mtr = new MeterVM();
                mtr.ID = obj.id;
                mtr.MeterName = obj.metername;
                lstmeter.Add(mtr);
            }
            return lstmeter;
        }
    }
}
