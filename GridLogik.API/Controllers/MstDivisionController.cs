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
    public class MstDivisionController : ApiController
    {
        IMstDivisionService _MstDivisionService;

        public MstDivisionController(IMstDivisionService MstDivisionService)
        {
            _MstDivisionService = MstDivisionService;
        }
        //Get api/mstdivision

        [Route("api/GetByIdForDivision/{id}")]
        public IQueryable<mstdivision> GetByIdForDivision(int id)
        {
            return _MstDivisionService.FindBy(m => m.mstregion.rgnrecid == id && (m.isdeleted == 0 || m.isdeleted == null));
        }
        public IQueryable<mstdivision> Get()
        {
            return _MstDivisionService.GetAll();
        }
        //Get api/mstdivision/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var mstdivisionmodel = await _MstDivisionService.Get(id);
            return Ok(mstdivisionmodel);
        }

        // POST api/mstdivision
        public async Task<IHttpActionResult> Post([FromBody]mstdivision _mstmodel)
        {
            Check(_mstmodel);
            var mstmodel = await _MstDivisionService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.divrecid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstdivision _mstmodel)
        {
            if (id != _mstmodel.divrecid)
            {
                throw new Exception("Invalid Division Model");
            }
            Check(_mstmodel);
            var mstmodel = await _MstDivisionService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _MstDivisionService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Division Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _MstDivisionService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void Check(mstdivision _mstdivision)
        {
            var check = _MstDivisionService.FindBy(x => x.divname.ToLower() == _mstdivision.divname.ToLower() && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Division Name Already Exists!");
            }
        }
    }
}
