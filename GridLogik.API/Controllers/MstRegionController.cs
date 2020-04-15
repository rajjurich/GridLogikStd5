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
    public class MstRegionController : ApiController
    {
        IMstRegionService _mstRegionService;

        public MstRegionController(IMstRegionService mstRegionService)
        {
            _mstRegionService = mstRegionService;
        }
        //Get api/mstregion
        
        public IQueryable<mstregion> Get()
        {
            return _mstRegionService.GetAll();
        }
        //Get api/GetByIdForRegion/1

        [Route("api/GetByIdForRegion/{id}")]
        public IQueryable<mstregion> GetByIdForRegion(int id)
        {
            return _mstRegionService.FindBy(m => m.mstzone.znrecid == id && (m.isdeleted == 0 || m.isdeleted == null));
        }
        public async Task<IHttpActionResult> Get(int id)
        {
            var mstregionmodel = await _mstRegionService.Get(id);
            return Ok(mstregionmodel);
        }

        // POST api/mstregion
        public async Task<IHttpActionResult> Post([FromBody]mstregion _mstmodel)
        {
            Check(_mstmodel);
            var mstmodel = await _mstRegionService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.rgnrecid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstregion _mstmodel)
        {
            if (id != _mstmodel.rgnrecid)
            {
                throw new Exception("Invalid Region Model");
            }
            Check(_mstmodel);
            var mstmodel = await _mstRegionService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _mstRegionService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Region Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _mstRegionService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void Check(mstregion _mstregion)
        {
            var check = _mstRegionService.FindBy(x => x.rgnname.ToLower() == _mstregion.rgnname.ToLower() && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Region Name Already Exists!");
            }
        }
    }
}
