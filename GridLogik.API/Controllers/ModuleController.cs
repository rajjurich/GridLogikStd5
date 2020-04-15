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
    public class ModuleController : ApiController
    {
        IModuleService _moduleService;

        

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        //Get api/Module

        public IQueryable<mstmodule> Get()
        {
            return _moduleService.GetAll().Where(model=>model.isdeleted==0 || model.isdeleted == null);
        }

        //Get api/Module/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var modulemodel = await _moduleService.Get(id);
            return Ok(modulemodel);
        }

        //Post Api/Module/mstmodule
        public async Task<IHttpActionResult> Post([FromBody]mstmodule _mstmodel)
        {
            CheckModuleName(_mstmodel);
            CheckModulePosition(_mstmodel);
            var mstmodel = await _moduleService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.id }, mstmodel);
        }

        //Put Api/Module/1/mstmodule
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstmodule _mstmodel)
        {
            if (id != _mstmodel.id)
            {
                throw new Exception("Invalid Module Model");
            }
            CheckModuleName(_mstmodel);
            CheckModulePosition(_mstmodel);
            var mstmodel = await _moduleService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/Module/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _moduleService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Module Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _moduleService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void CheckModuleName(mstmodule _mstmodule)
        {
            var check = _moduleService.FindBy(x => x.modulename.ToLower() == _mstmodule.modulename.ToLower() && x.id != _mstmodule.id && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Module Name Already Exists!");
            }
        }

        private void CheckModulePosition(mstmodule _mstmodule)
        {
            var check = _moduleService.FindBy(x => x.moduleposition == _mstmodule.moduleposition && x.id != _mstmodule.id && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Module Position Already Exists!");
            }
        }

    }
}
