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
    public class MeterTypeController : ApiController
    {
        IMeterTypeService _metertypeservice;

        public MeterTypeController(IMeterTypeService metertypeservice)
        {
            _metertypeservice = metertypeservice;
        }
        //Get api/metertype

        public IQueryable<metertype> Get()
        {
            return _metertypeservice.GetAll();
        }
        //Get api/metertype/1

        public async Task<IHttpActionResult> Get(int id)
        {
            var metertypemodel = await _metertypeservice.Get(id);
            return Ok(metertypemodel);
        }

        // POST api/metertype
        public async Task<IHttpActionResult> Post([FromBody]metertype _mstmodel)
        {
            Check(_mstmodel);

            var mstmodel = await _metertypeservice.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.id }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]metertype _mstmodel)
        {
            if (id != _mstmodel.id)
            {
                throw new Exception("Invalid MeterType Model");
            }

            Check(_mstmodel);
            var mstmodel = await _metertypeservice.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        private void Check(metertype _meterType)
        {
            var check = _metertypeservice.FindBy(x => x.metertypename.ToLower() == _meterType.metertypename.ToLower() && x.id != _meterType.id && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Meter Name Already Exists!");
            }
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _metertypeservice.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid MeterType Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _metertypeservice.Delete(mstmodel);
            return Ok(mstpassmodel);
        }
    }
}
