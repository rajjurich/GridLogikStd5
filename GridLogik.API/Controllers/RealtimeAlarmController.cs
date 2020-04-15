using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Entities;
using Domain.Services;

namespace GridLogik.API.Controllers
{
    public class RealtimeAlarmController : ApiController
    {
        IRealtimeAlarmService _IRealTimeServiceobj;

        public RealtimeAlarmController(IRealtimeAlarmService IRealTimeServiceobj)
        {
            _IRealTimeServiceobj = IRealTimeServiceobj;
        }
        //Get api/rtalarm
        
        public IQueryable<rtalarm> Get()
        {
            return _IRealTimeServiceobj.GetAll();
        }
        //Get api/rtalarm/1
        
        public async Task<IHttpActionResult> Get(int id)
        {
            var rtalarmmodel = await _IRealTimeServiceobj.Get(id);
            return Ok(rtalarmmodel);
        }

        

        // POST api/rtalarm
        public async Task<IHttpActionResult> Post([FromBody]rtalarm _mstmodel)
        {
            var mstmodel = await _IRealTimeServiceobj.Add(_mstmodel);

            return CreatedAtRoute("DefaultApi", new { id = mstmodel.id }, mstmodel);
        }

        // PUT api/mstmodel/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]rtalarm _mstmodel)
        {
            if (id != _mstmodel.id)
            {
                throw new Exception("Invalid RealTime Model");
            }
            var mstmodel = await _IRealTimeServiceobj.Edit(_mstmodel);

            return Ok(mstmodel);
        }

        // DELETE api/mstmodel/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var mstmodel = await _IRealTimeServiceobj.Get(id);
            if (mstmodel == null)
            {
                throw new Exception("Invalid RealTime Model");
            }
            mstmodel.isdeleted = 1;
            var mstpassmodel = await _IRealTimeServiceobj.Delete(mstmodel);
            return Ok(mstpassmodel);
        }

        [HttpPost]
        [Route("api/RealtimeAlarm/UpdateData")]
        public async Task<IHttpActionResult> UpdateData([FromBody]IEnumerable<rtalarm> _mstmodel)
        {
            foreach (var modelstandard in _mstmodel)
            {
                var rtalarmmodel = await _IRealTimeServiceobj.Get(Convert.ToInt32(modelstandard.id));
                rtalarmmodel.status = modelstandard.status;
                rtalarmmodel.sendsms = modelstandard.sendsms;
                rtalarmmodel.sendemail = modelstandard.sendemail;
                rtalarmmodel.givepopup = modelstandard.givepopup;
                var mstmodel = await _IRealTimeServiceobj.Edit(rtalarmmodel);
            }
            return Ok(_mstmodel);
        }

        [Route("api/RealtimeAlarm/AlramNameExits/{name}")]
        public IQueryable<rtalarm> GetAlarmName(string name)
        {
            var realalarmmodel = _IRealTimeServiceobj.GetAll().Where(x => x.alarmname == name);
            return realalarmmodel;
        }
    }
}
