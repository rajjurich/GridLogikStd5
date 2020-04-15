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
    public class MstSubstationController : ApiController
    {
        IMstSubstationService _MstSubstationService;

        public MstSubstationController(IMstSubstationService MstSubstationService)
        {
            _MstSubstationService = MstSubstationService;
        }
        //Get api/mstsubstation
        
        public IQueryable<mstsubstation> Get()
        {
            return _MstSubstationService.GetAll();
        }
        //Get api/mstsubstation/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var mstsubstationmodel = await _MstSubstationService.Get(id);
            return Ok(mstsubstationmodel);
        }

        // POST api/mstsubstation
        public async Task<IHttpActionResult> Post([FromBody]mstsubstation _mstmodel)
        {
            Check(_mstmodel);
            var mstmodel = await _MstSubstationService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.ssrecid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstsubstation _mstmodel)
        {
            if (id != _mstmodel.ssrecid)
            {
                throw new Exception("Invalid Substation Model");
            }
            Check(_mstmodel);
            var mstmodel = await _MstSubstationService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _MstSubstationService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Substation Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _MstSubstationService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void Check(mstsubstation _mstsubstation)
        {
            var check = _MstSubstationService.FindBy(x => x.ssname.ToLower() == _mstsubstation.ssname.ToLower() && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Substation Name Already Exists!");
            }
        }
    }
}
