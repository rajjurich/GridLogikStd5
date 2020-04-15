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
    public interface IRoleMenuAccessService
    {
        Task<mstrolemenuaccess> Add(mstrolemenuaccess entity);
        Task<IEnumerable<mstrolemenuaccess>> AddRange(IEnumerable<mstrolemenuaccess> entities);
        Task<mstrolemenuaccess> Delete(mstrolemenuaccess entity);
        Task<mstrolemenuaccess> Edit(mstrolemenuaccess entity);
        Task<mstrolemenuaccess> Get(int Key);
        IQueryable<mstrolemenuaccess> GetAll();
        Task<mstrolemenuaccess> Remove(mstrolemenuaccess entity);
        IQueryable<mstrolemenuaccess> FindBy(Expression<Func<mstrolemenuaccess, bool>> predicate);
        Task<IEnumerable<mstrolemenuaccess>> RemoveRange(IEnumerable<mstrolemenuaccess> entities);

        IQueryable<mstrolemenuaccess> GetRoleMenuAccessByRole(int Id);

    }
    public class RoleMenuAccessService : IRoleMenuAccessService
    {
        IEntityRepository<mstrolemenuaccess> entityRepository;
        IUnitOfWork entity;
        etools_devEntities db;
        public RoleMenuAccessService(IEntityRepository<mstrolemenuaccess> entityRepository, IUnitOfWork entity
            , DbContext db)
        {
            this.entityRepository = entityRepository;
            this.entity = entity;
            this.db = (etools_devEntities)db;
        }
        public Task<mstrolemenuaccess> Add(mstrolemenuaccess entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstrolemenuaccess>> AddRange(IEnumerable<mstrolemenuaccess> entities)
        {
            return entityRepository.AddRange(entities);
        }

        public Task<mstrolemenuaccess> Delete(mstrolemenuaccess entity)
        {
            throw new NotImplementedException();
        }

        public Task<mstrolemenuaccess> Edit(mstrolemenuaccess entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<mstrolemenuaccess> Get(int Key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstrolemenuaccess> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<mstrolemenuaccess> Remove(mstrolemenuaccess entity)
        {
            var x = entityRepository.Remove(entity);

            return x;
        }

        public IQueryable<mstrolemenuaccess> FindBy(Expression<Func<mstrolemenuaccess, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstrolemenuaccess>> RemoveRange(IEnumerable<mstrolemenuaccess> entities)
        {
            return entityRepository.RemoveRange(entities);
        }


        public IQueryable<mstrolemenuaccess> GetRoleMenuAccessByRole(int Id)
        {

            var selection = (from rm in db.mstroles.Where(rm => (rm.rolisdeleted == 0 || rm.rolisdeleted == null) && rm.rolrecid == Id)
                             from mnu in db.mstmenus.Where(mnu => mnu.mnuisdeleted == 0 || mnu.mnuisdeleted == null)
                             from rma in db.mstrolemenuaccesses.Where(rma => rma.rmaroleid == rm.rolrecid && mnu.mnurecid == rma.rmamnuid).DefaultIfEmpty()
                             //join mnu in dbcontext.mstmenus on mnu. //.Where(m => m.mnuid == rma.rmamnuid).DefaultIfEmpty()
                             //from rma in dbcontext.mstrolemenuaccesses.Where(rma => rma.rmaroleid == rm.rolid || rma.rmamnuid == m.mnuid).DefaultIfEmpty()
                             select new
                             {
                                 rmacreateaccess = (rma.rmacreateaccess != null ? rma.rmacreateaccess : 0),
                                 rmadeleteaccess = (rma.rmadeleteaccess != null ? rma.rmadeleteaccess : 0),
                                 rmaisdeleted = 0,
                                 rmamnuid = mnu.mnurecid,
                                 rmareadaccess = (rma.rmareadaccess != null ? rma.rmareadaccess : 0),
                                 rmarecid = rma.rmarecid,
                                 rmaroleid = rm.rolrecid,
                                 rmaupdateaccess = (rma.rmaupdateaccess != null ? rma.rmaupdateaccess : 0),
                                 mstmenu = new
                                 {
                                     mnurecid = mnu.mnurecid,
                                     mnuitemname = mnu.mnuitemname
                                 }
                             }).Distinct().ToList()
                             .Select(x => new mstrolemenuaccess
                             {
                                 rmacreateaccess = x.rmacreateaccess,
                                 rmadeleteaccess = x.rmadeleteaccess,
                                 rmaisdeleted = 0,
                                 rmamnuid = x.rmamnuid,
                                 rmareadaccess = x.rmareadaccess,
                                 rmarecid = x.rmarecid,
                                 rmaroleid = x.rmaroleid,
                                 rmaupdateaccess = x.rmaupdateaccess,
                                 mstmenu = new mstmenu
                                 {
                                     mnurecid = x.mstmenu.mnurecid,
                                     mnuitemname = x.mstmenu.mnuitemname
                                 }

                             })
                             .AsQueryable();

            var xxx = selection.ToList();

            return selection;
        }
    }
}
