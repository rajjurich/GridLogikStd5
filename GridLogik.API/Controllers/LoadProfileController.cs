using Domain.Model;
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
    public class LoadProfileController : ApiController
    {
        ILoadProfileService loadProfileService;
        public LoadProfileController(ILoadProfileService loadProfileService)
        {
            this.loadProfileService = loadProfileService;
        }
        // GET api/loadprofile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/LoadProfile/GetDayWiseData/{MeterID}/{Month}/{Year}/{param}")]
        public async Task<IHttpActionResult> GetDayWiseData(long MeterID, long Month, long Year, string param)
        {
            IEnumerable<Load> lst = await loadProfileService.GetDayWiseData(MeterID, Month, Year, param);
            return Ok(lst);
        }

        [Route("api/LoadProfile/CompareDayWise/{MeterID}/{FromMonth}/{FromYear}/{ToMonth}/{ToYear}/{Parameter}")]
        public async Task<IHttpActionResult> GetCompareDayWise(long MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear, string Parameter)
        {
            IEnumerable<ConsumptionCompare> lst = await loadProfileService.CompareDayWise(MeterID, FromMonth, FromYear, ToMonth, ToYear, Parameter);
            return Ok(lst);
        }
        // GET api/loadprofile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/loadprofile
        public void Post([FromBody]string value)
        {
        }

        // PUT api/loadprofile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/loadprofile/5
        public void Delete(int id)
        {
        }
    }
}
