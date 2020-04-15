using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class MenuController : ApiController
    {
        IMenuService menuService;
        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        // GET api/menu
        public IQueryable<mstmenu> Get()
        {
            var menus = menuService.GetAll().Where(model => model.mnuisdeleted == 0 || model.mnuisdeleted == null);

            return menus;
        }

        [Route("api/menu/GetByUserId/{Id}")]
        public IQueryable<mstmenu> GetByUserId(int Id)
        {
            //var x = _menuService.GetByUserId(1).ToList();            
            return menuService.GetByUserId(Id);
        }

        // GET api/menu/5
        public Task<mstmenu> Get(int id)
        {
            return menuService.Get(id);
        }

        // POST api/menu
        public async Task<IHttpActionResult> Post([FromBody]mstmenu mstmenu)
        {
            CheckItemPosition(mstmenu);
            var menumodel = await menuService.Add(mstmenu);
            return CreatedAtRoute("DefaultApi", new { id = mstmenu.mnurecid }, mstmenu);
        }

        private void CheckItemPosition(mstmenu mstmenu)
        {
            var exists = menuService.FindBy(x => x.mnuitemposition == mstmenu.mnuitemposition 
                && x.mnumodulid == mstmenu.mnumodulid
                && x.mnurecid != mstmenu.mnurecid
                ).Any();
            if (exists)
            {
                throw new Exception("Item Position already Exists");
            }
        }

        // PUT api/menu/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstmenu mstmenu)
        {
            CheckItemPosition(mstmenu);
            if (id != mstmenu.mnurecid)
            {
                throw new Exception("Invalid Menu Model");
            }
            var mstMenu = await menuService.Edit(mstmenu);

            return Ok(mstMenu);
        }

        // DELETE api/menu/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await menuService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Menu Model");
            }
            mstmodel.mnuisdeleted = 1;
            var mstpassmodel = await menuService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }
    }
}
