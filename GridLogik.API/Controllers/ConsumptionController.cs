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
    public class ConsumptionController : ApiController
    {
        IConsumptionService consumptionService;
        public ConsumptionController(IConsumptionService consumptionService)
        {
            this.consumptionService = consumptionService;
        }
        // GET api/consumption
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/consumption/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Consumption/GetMonthWiseData/{MeterID}/{FromMonth}/{FromYear}/{ToMonth}/{ToYear}")]
        public async Task<IHttpActionResult> GetMonthWiseData(string MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear)
        {
            IEnumerable<Consumption> lst = await consumptionService.GetMonthWiseData(MeterID, FromMonth, FromYear, ToMonth, ToYear);
            return Ok(lst);
        }

        [Route("api/Consumption/GetDayWiseData/{MeterID}/{Month}/{Year}")]
        public async Task<IHttpActionResult> GetDayWiseData(string MeterID, long Month, long Year)
        {
            IEnumerable<Consumption> lst = await consumptionService.GetDayWiseData(MeterID, Month, Year);
            return Ok(lst);
        }

        [Route("api/Consumption/GetHourWiseData/{MeterID}/{Day}/{Month}/{Year}")]
        public async Task<IHttpActionResult> GetHourWiseData(string MeterID, long Day, long Month, long Year)
        {
            IEnumerable<Consumption> lst = await consumptionService.GetHourWiseData(MeterID, Day, Month, Year);
            return Ok(lst);
        }

        [Route("api/Consumption/CompareBlockWise/{MeterID}/{CompareDate}/{WithDate}")]
        public async Task<IHttpActionResult> GetCompareBlockWise(string MeterID, string compareDate, string withDate)
        {
            IEnumerable<ConsumptionCompare> lst = await consumptionService.CompareBlockWise(MeterID, compareDate, withDate);
            return Ok(lst);
        }

        [Route("api/Consumption/CompareHourWise/{MeterID}/{CompareDate}/{WithDate}")]
        public async Task<IHttpActionResult> GetCompareHourWise(string MeterID, string CompareDate, string WithDate)
        {
            IEnumerable<ConsumptionCompare> lst = await consumptionService.CompareHourWise(MeterID, CompareDate, WithDate);
            return Ok(lst);
        }
        [Route("api/Consumption/CompareDayWise/{MeterID}/{FromMonth}/{FromYear}/{ToMonth}/{ToYear}")]
        public async Task<IHttpActionResult> GetCompareDayWise(string MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear)
        {
            IEnumerable<ConsumptionCompare> lst = await consumptionService.CompareDayWise(MeterID, FromMonth, FromYear, ToMonth, ToYear);
            return Ok(lst);
        }

        [Route("api/Consumption/CompareYearWise/{MeterID}/{FromYear}/{ToYear}")]
        public async Task<IHttpActionResult> GetCompareYearWise(string MeterID, long FromYear, long ToYear)
        {
            IEnumerable<ConsumptionCompare> lst = await consumptionService.CompareYearWise(MeterID, FromYear, ToYear);
            return Ok(lst);
        }

        // POST api/consumption
        public void Post([FromBody]string value)
        {
        }

        // PUT api/consumption/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/consumption/5
        public void Delete(int id)
        {
        }
    }
}
