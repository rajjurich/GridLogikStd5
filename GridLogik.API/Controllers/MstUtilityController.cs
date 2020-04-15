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
    public class MstUtilityController : ApiController
    {
        ImstutilityService _mstUtilityService;

        public MstUtilityController(ImstutilityService mstUtilityService)
        {
            _mstUtilityService = mstUtilityService;
        }
        //Get api/mstutility

        public IQueryable<mstutility> Get()
        {
            return _mstUtilityService.GetAll().Where(x => x.isdeleted == 0 || x.isdeleted == null);
        }
        //Get api/mstutility/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var mstutilitymodel = await _mstUtilityService.Get(id);
            return Ok(mstutilitymodel);
        }

        // POST api/mstutility
        public async Task<IHttpActionResult> Post([FromBody]mstutility _mstmodel)
        {
            Check(_mstmodel);
            var mstmodel = await _mstUtilityService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.utilrecid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstutility _mstmodel)
        {
            if (id != _mstmodel.utilrecid)
            {
                throw new Exception("Invalid Utilities Model");
            }
            Check(_mstmodel);
            var mstmodel = await _mstUtilityService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _mstUtilityService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Utilities Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _mstUtilityService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void Check(mstutility _mstutility)
        {
            var check = _mstUtilityService.FindBy(x => x.utilname.ToLower() == _mstutility.utilname.ToLower() && x.utilrecid != _mstutility.utilrecid && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Utility Name Already Exists!");
            }
        }
    }
}
