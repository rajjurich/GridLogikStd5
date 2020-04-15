using Domain.Core;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Domain.Services
{
    public interface IMenuService
    {
        Task<mstmenu> Add(mstmenu entity);
        Task<IEnumerable<mstmenu>> AddRange(IEnumerable<mstmenu> entities);
        Task<mstmenu> Delete(mstmenu entity);
        Task<mstmenu> Edit(mstmenu entity);
        Task<mstmenu> Get(int Key);
        IQueryable<mstmenu> GetAll();
        Task<mstmenu> Remove(mstmenu entity);
        IQueryable<mstmenu> FindBy(Expression<Func<mstmenu, bool>> predicate);
        Task<IEnumerable<mstmenu>> RemoveRange(IEnumerable<mstmenu> entities);

        IQueryable<mstmenu> GetByUserId(int Id);
    }
    public class MenuService : IMenuService
    {
        IEntityRepository<mstmenu> _entityRepository;
        IUnitOfWork _entity;
        etools_devEntities _db;
        public MenuService(IEntityRepository<mstmenu> entityRepository, IUnitOfWork entity, DbContext db)
        {
            _entityRepository = entityRepository;
            _entity = entity;
            _db = (etools_devEntities)db;
        }

        public Task<mstmenu> Add(mstmenu entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<mstmenu>> AddRange(IEnumerable<mstmenu> entities)
        {
            throw new NotImplementedException();
        }

        public Task<mstmenu> Delete(mstmenu entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<mstmenu> Edit(mstmenu entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<mstmenu> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<mstmenu> GetAll()
        {
            return _entityRepository.GetAll().Include(x => x.mstmodule);
        }

        public Task<mstmenu> Remove(mstmenu entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<mstmenu> FindBy(Expression<Func<mstmenu, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<mstmenu>> RemoveRange(IEnumerable<mstmenu> entities)
        {
            throw new NotImplementedException();
        }


        public IQueryable<mstmenu> GetByUserId(int Id)
        {
            //var menus = _db.Database.SqlQuery<IQueryable<mstmenu>>("select * from mstmenu").AsQueryable();

            var menus = (from users in _db.mstusers
                         join rolemenuaccess in _db.mstrolemenuaccesses on users.usrroleid equals rolemenuaccess.rmaroleid
                         join menu in _db.mstmenus on rolemenuaccess.rmamnuid equals menu.mnurecid
                         join module in _db.mstmodules on menu.mnumodulid equals module.id
                         where (users.usrrecid == Id) && ((module.isdeleted == null) || (module.isdeleted == 0)) && ((menu.mnuisdeleted == null) || (menu.mnuisdeleted == 0)) && rolemenuaccess.rmareadaccess == 1 && users.usrisdeleted == 0
                         orderby module.moduleposition, menu.mnuitemposition
                         select new
                         {
                             link = menu.link,
                             mnuid = menu.mnurecid,
                             mnuisdeleted = menu.mnuisdeleted,
                             mnuitemname = menu.mnuitemname,
                             mnuitemposition = menu.mnuitemposition,
                             mnumodulid = menu.mnumodulid,
                             mnurecid = menu.mnurecid,
                             mnutype = menu.mnutype,
                             mstmodule = new
                             {
                                 id = module.id,
                                 isdeleted = module.isdeleted,
                                 link = module.link,
                                 modulename = module.modulename,
                                 moduleposition = module.moduleposition
                             }
                         }).ToList()
                         .Select(x => new mstmenu
                         {

                             link = x.link,
                             //mnuid = x.mnuid,
                             mnuisdeleted = x.mnuisdeleted,
                             mnuitemname = x.mnuitemname,
                             mnuitemposition = x.mnuitemposition,
                             mnumodulid = x.mnumodulid,
                             mnurecid = x.mnurecid,
                             mnutype = x.mnutype,
                             mstmodule = new mstmodule
                             {
                                 id = x.mstmodule.id,
                                 isdeleted = x.mstmodule.isdeleted,
                                 link = x.mstmodule.link,
                                 modulename = x.mstmodule.modulename,
                                 moduleposition = x.mstmodule.moduleposition
                             }
                         })

                         .AsQueryable();


            //var menu = _entityRepository.GetAll()
            //    //.Include(x => x.mstmodule)
            //    .Include(x => x.mstrolemenuaccesses)
            //    .Include(x => x.mstrolemenuaccesses.Select(y => y.mstrole))
            //    .Include(x => x.mstrolemenuaccesses.Select(y => y.mstrole).Select(z => z.mstusers))
            //    .Where(x => x.mstrolemenuaccesses.Select(y => y.mstrole).Any(z => z.mstusers.Any(u => u.usrrecid == Id)) && x.mnuisdeleted == 0)
            //    //.Take(1)





            ////.Any(z => z.mstusers.Where(u => u.usrrecid == Id)))
            //    //.Where(x => x.mstrolemenuaccesses.Select(y => y.mstrole).Select(z => z.mstusers))
            //;


            return menus;
            //.Include(x => x.MenuAccesses)
            //.Include(x => x.MenuAccesses.Select(y => y.Role))
            //.Where(x => x.MenuAccesses.Any(z => z.Role.RoleName == roleName));
        }


    }
}
