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
    public class StandardAlaramController : ApiController
    {
        IStandardAlaramService _StandardAlaramService;

        public StandardAlaramController(IStandardAlaramService StandardAlaramService)
        {
            _StandardAlaramService = StandardAlaramService;
        }
        //Get api/standardalarm
        
        public IQueryable<standardalarm> Get()
        {
            return _StandardAlaramService.GetAll().Where(model => model.isdeleted == 0 || model.isdeleted == null);
        }
        //Get api/standardalarm/1
        
        public async Task<IHttpActionResult> Get(int id)
        {
            var standardalarmmodel = await _StandardAlaramService.Get(id);
            return Ok(standardalarmmodel);
        }

        [Route("api/StandardAlaram/AlramNameExits/{name}")]
        public IQueryable<standardalarm> GetAlarmName(string name)
        {
            var standardalarmmodel = _StandardAlaramService.GetAll().Where(x => x.alarmname == name);
            return standardalarmmodel;
        }
        [HttpPost]
        [Route("api/StandardAlaram/UpdateData")]
        public async Task<IHttpActionResult> UpdateData([FromBody]IEnumerable<standardalarm> _mstmodel)
        {
            foreach (var modelstandard in _mstmodel)
            {
                var standardalarmmodel = await _StandardAlaramService.Get(Convert.ToInt32(modelstandard.id));
                standardalarmmodel.status = modelstandard.status;
                standardalarmmodel.sendsms = modelstandard.sendsms;
                standardalarmmodel.sendemail = modelstandard.sendemail;
                standardalarmmodel.givepopup = modelstandard.givepopup;
                var mstmodel = await _StandardAlaramService.Edit(standardalarmmodel);
            }
            
            return Ok(_mstmodel);
        }

        // POST api/standardalarm
        public async Task<IHttpActionResult> Post([FromBody]standardalarm _mstmodel)
        {
            var mstmodel = await _StandardAlaramService.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.id }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]standardalarm _mstmodel)
        {
            if (id != _mstmodel.id)
            {
                throw new Exception("Invalid Standard Model");
            }
            var mstmodel = await _StandardAlaramService.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _StandardAlaramService.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid Standard Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _StandardAlaramService.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

    }
}
