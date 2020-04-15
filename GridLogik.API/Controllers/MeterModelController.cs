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
    public class MeterModelController : ApiController
    {
        IMeterModelService _meterModelService;
        public MeterModelController(IMeterModelService meterModelService)
        {
            _meterModelService = meterModelService;
        }
        // GET api/metermodel
        [HttpGet]
        public IQueryable<metermodel> Get()
        {
            return _meterModelService.GetAll().OrderBy(x => x.modelname);
        }

        // GET api/metermodel/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var meterModel = await _meterModelService.Get(id);
            return Ok(meterModel);
        }

        // POST api/metermodel
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]metermodel _metermodel)
        {
            Check(_metermodel);

            var metermodel = await _meterModelService.Add(_metermodel);

            return CreatedAtRoute("DefaultApi", new { id = metermodel.id }, metermodel);
        }

        // PUT api/metermodel/5
        [HttpPut]            
        public async Task<IHttpActionResult> Put(int id, [FromBody]metermodel _metermodel)
        {
            Check(_metermodel);

            if (id != _metermodel.id)
            {
                throw new Exception("Invalid Meter Model");
            }
            var metermodel = await _meterModelService.Edit(_metermodel);

            return Ok(metermodel);
        }

        private void Check(metermodel _metermodel)
        {
            var check = _meterModelService.FindBy(x => x.modelname.ToUpper() == _metermodel.modelname.ToUpper() && x.id != _metermodel.id && (x.isdeleted == 0 || x.isdeleted == null)).Count() > 0;
            if (check)
            {
                throw new Exception("Meter Model Already Exists!");
            }
        }

        // DELETE api/metermodel/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _metermodel = await _meterModelService.Get(id);
            if (_metermodel == null)
            {
                throw new Exception("Invalid Meter Model");
            }            
            var metermodel = await _meterModelService.Delete(_metermodel);
            return Ok(metermodel);
        }
    }
}
