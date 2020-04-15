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
    public interface IMenuAccessService
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

        Task<mstrolemenuaccess> GetByUserId(string link, int Id);
    }
    public class MenuAccessService : IMenuAccessService
    {
        etools_devEntities _db;
        public MenuAccessService(DbContext db)
        {
            _db = (etools_devEntities)db;
        }
        public Task<mstrolemenuaccess> Add(mstrolemenuaccess entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<mstrolemenuaccess>> AddRange(IEnumerable<mstrolemenuaccess> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstrolemenuaccess> Delete(mstrolemenuaccess entity)
        {
            throw new NotImplementedException();
        }

        public Task<mstrolemenuaccess> Edit(mstrolemenuaccess entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IQueryable<mstrolemenuaccess> FindBy(Expression<Func<mstrolemenuaccess, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<mstrolemenuaccess>> RemoveRange(IEnumerable<mstrolemenuaccess> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<mstrolemenuaccess> GetByUserId(string link, int Id)
        {
            //mstrolemenuaccess selection = null;
            var mstrolemenuaccess = await (from user in _db.mstusers
                                     join menuaccess in _db.mstrolemenuaccesses on user.usrroleid equals menuaccess.rmaroleid
                                     join menu in _db.mstmenus on menuaccess.rmamnuid equals menu.mnurecid
                                     where (menu.link.ToUpper() == link.ToUpper()) && (user.usrrecid == Id) 
                                     && (menu.mnuisdeleted == 0 || menu.mnuisdeleted == null)
                                     && (menuaccess.rmaisdeleted == 0 || menuaccess.rmaisdeleted == null) 
                                     && (user.usrisdeleted == 0 || user.usrisdeleted == null)
                                     select menuaccess).FirstOrDefaultAsync();

            return mstrolemenuaccess;
        }
    }
}
