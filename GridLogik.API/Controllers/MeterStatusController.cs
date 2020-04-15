using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class MeterStatusController : ApiController
    {
        IMeterStatusService _meterStatusService;
        public MeterStatusController(IMeterStatusService meterStatusService)
        {
            _meterStatusService = meterStatusService;
        }
        // GET api/meterstatus
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/meterstatus/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/meterstatus
        public void Post([FromBody]string value)
        {
        }

        // PUT api/meterstatus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/meterstatus/5
        public void Delete(int id)
        {
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MeterStatus/GetMeterStatusData")]
        public IHttpActionResult GetMeterStatusData(string iGroupID, string iStatus)
        {

            Tuple<List<MeterStatus>, int> tuplelistMeterStatus;

            tuplelistMeterStatus = _meterStatusService.GetMeterStatusUtilities(iGroupID, iStatus);


            return Ok(tuplelistMeterStatus);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MeterStatus/GetMeterStatus")]
        public IHttpActionResult GetMeterStatus()
        {

            //Tuple<List<MeterStatus>, int> tuplelistMeterStatus;

            var result = _meterStatusService.GetMeterStatus();


            return Ok(result);
        }
        
    }
}
