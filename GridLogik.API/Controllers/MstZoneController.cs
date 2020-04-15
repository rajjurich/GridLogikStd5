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
    public class MstZoneController : ApiController
    {
        IMstzoneService _mstzoneService;

        public MstZoneController(IMstzoneService mstzoneService)
        {
            _mstzoneService = mstzoneService;
        }
        //Get api/mstzone
        
        public IQueryable<mstzone> Get()
        {
            return _mstzoneService.GetAll();
        }
        [Route("api/GetByIdForDropdown/{id}")]
        public IQueryable<mstzone> GetByIdForDropdown(int id)
        {
            return _mstzoneService.FindBy(m=>m.znutilityid==id && (m.isdeleted==0 || m.isdeleted==null));
        }
        //Get api/mstzone/1

        

        public async Task<IHttpActionResult> Get(int id)
        {
            var mstzonemodel = await _mstzoneService.Get(id);
            return Ok(mstzonemodel);
        }

        // POST api/mstzone
        public async Task<IHttpActionResult> Post([FromBody]mstzone _mstmodel)
        {
            Check(_mstmodel);
            var mstmodel = await _mstzoneService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.znrecid }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]mstzone _mstmodel)
        {
            if (id != _mstmodel.znrecid)
            {
                throw new Exception("Invalid Zone Model");
            }
            CheckEdit(_mstmodel);
            var mstmodel = await _mstzoneService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _mstzoneService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Zone Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _mstzoneService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        private void Check(mstzone _mstzone)
        {
            var check = _mstzoneService.FindBy(x => x.znname.ToLower() == _mstzone.znname.ToLower() && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Zone Name Already Exists!");
            }
        }

        private void CheckEdit(mstzone _mstzone)
        {
            
            var check = _mstzoneService.FindBy(x => x.znname.ToLower() == _mstzone.znname.ToLower() && x.znid == _mstzone.znid).Count();

            var check2 = _mstzoneService.FindBy(x => x.znname.ToLower() == _mstzone.znname.ToLower() && x.znid != _mstzone.znid).Count();

            if (check == 0 && check2 > 0)
            {
                throw new Exception("Zone Name Already Exists!");
            }
        }
    }
}
