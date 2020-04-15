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
    public interface IOPCServerTagService
    {
        Task<opc_server_tag> Add(opc_server_tag entity);
        Task<IEnumerable<opc_server_tag>> AddRange(IEnumerable<opc_server_tag> entities);
        Task<opc_server_tag> Delete(opc_server_tag entity);
        Task<opc_server_tag> Edit(opc_server_tag entity);
        Task<opc_server_tag> Get(int Key);
        IQueryable<opc_server_tag> GetAll();

        
        Task<opc_server_tag> Remove(opc_server_tag entity);
        IQueryable<opc_server_tag> FindBy(Expression<Func<opc_server_tag, bool>> predicate);
        Task<IEnumerable<opc_server_tag>> RemoveRange(IEnumerable<opc_server_tag> entities);
    }
    public class OPCServerTagService : IOPCServerTagService
    {
        IEntityRepository<opc_server_tag> entityRepository;
        etools_devEntities _db;
        public OPCServerTagService(IEntityRepository<opc_server_tag> entityRepository, DbContext db)
        {
            this.entityRepository = entityRepository;
            _db = (etools_devEntities)db;
        }
        public Task<opc_server_tag> Add(opc_server_tag entity)
        {
            return entityRepository.Add(entity);
        }

        public Task<IEnumerable<opc_server_tag>> AddRange(IEnumerable<opc_server_tag> entities)
        {
            throw new NotImplementedException();
        }

        public Task<opc_server_tag> Delete(opc_server_tag entity)
        {
            return entityRepository.Delete(entity);
        }

        public Task<opc_server_tag> Edit(opc_server_tag entity)
        {
            return entityRepository.Edit(entity);
        }

        public Task<opc_server_tag> Get(int Key)
        {
            return entityRepository.Get(Key);
        }

        public IQueryable<opc_server_tag> GetAll()
        {
            return entityRepository.GetAll();
        }

        

        public Task<opc_server_tag> Remove(opc_server_tag entity)
        {
            return entityRepository.Remove(entity);
        }

        public IQueryable<opc_server_tag> FindBy(Expression<Func<opc_server_tag, bool>> predicate)
        {
            return entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<opc_server_tag>> RemoveRange(IEnumerable<opc_server_tag> entities)
        {
            throw new NotImplementedException();
        }
    }
}
