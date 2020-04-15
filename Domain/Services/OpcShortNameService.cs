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
    public interface IOpcShortNameService
    {
        Task<opc_metername> Add(opc_metername entity);
        Task<IEnumerable<opc_metername>> AddRange(IEnumerable<opc_metername> entities);
        Task<opc_metername> Delete(opc_metername entity);
        Task<opc_metername> Edit(opc_metername entity);
        Task<opc_metername> Get(int Key);
        IQueryable<clsOpcname> GetAll();
        IQueryable<meter> GetAllOPCNAME();

        IQueryable<meter> GetAllOPCNAMEWithId(int id);
        Task<opc_metername> Remove(opc_metername entity);
        IQueryable<opc_metername> FindBy(Expression<Func<opc_metername, bool>> predicate);
        Task<IEnumerable<opc_metername>> RemoveRange(IEnumerable<opc_metername> entities);
    }
    public class OpcShortNameService : IOpcShortNameService
    {
        IEntityRepository<opc_metername> entityRepository;
        IUnitOfWork entity;
        etools_devEntities db;

        public OpcShortNameService(IEntityRepository<opc_metername> entityRepository, IUnitOfWork entity
            , DbContext db)
        {
            this.entityRepository = entityRepository;
            this.entity = entity;
            this.db = (etools_devEntities)db;
        }



        public IQueryable<meter> GetAllOPCNAME()
        {
            var selection = (from m in db.meters
                             join op in db.opc_metername on m.id equals op.meterid into pp
                             from op in pp.DefaultIfEmpty()
                             where op == null


                             select new
                             {
                                 m.id,
                                 m.metername
                             }).ToList().Select
                             (x => new meter
                             {
                                 id = x.id,
                                 metername = x.metername
                             }).AsQueryable();

            return selection;
        }

        public IQueryable<meter> GetAllOPCNAMEWithId(int id)
        {
            var selection = (from m in db.meters
                             join op in db.opc_metername on m.id equals op.meterid into pp
                             from op in pp.DefaultIfEmpty()
                             where op == null || op.id == id


                             select new
                             {
                                 m.id,
                                 m.metername
                             }).ToList().Select
                             (x => new meter
                             {
                                 id = x.id,
                                 metername = x.metername
                             }).AsQueryable();

            return selection;
        }
        public Task<opc_metername> Add(opc_metername entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<opc_metername>> AddRange(IEnumerable<opc_metername> entities)
        {
            throw new NotImplementedException();
        }

        public Task<opc_metername> Delete(opc_metername entity)
        {
            return entityRepository.Delete(entity);
        }

        public Task<opc_metername> Edit(opc_metername entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<opc_metername> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<clsOpcname> GetAll()
        {
            var selection = (from op in db.opc_metername
                             join m in db.meters on op.meterid equals m.id
                             select new
                             {
                                 op.opc_shortname,
                                 m.metername,
                                 op.id
                             }).ToList().Select(x => new clsOpcname
            {
                mopname=x.metername,
                id=x.id,
                opc_shortname=x.opc_shortname
            }).AsQueryable();

            return selection;
        }

        public Task<opc_metername> Remove(opc_metername entity)
        {
            return entityRepository.Remove(entity);
        }

        public IQueryable<opc_metername> FindBy(Expression<Func<opc_metername, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<opc_metername>> RemoveRange(IEnumerable<opc_metername> entities)
        {
            throw new NotImplementedException();
        }
    }
}
