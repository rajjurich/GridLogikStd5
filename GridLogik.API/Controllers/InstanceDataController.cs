using Domain.Common;
using Domain.Entities;
using Domain.Model;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class InstanceDataController : ApiController
    {
        IInstanceDataService instanceDataService;
        public InstanceDataController(IInstanceDataService instanceDataService)
        {
            this.instanceDataService = instanceDataService;
        }
        // GET api/instancedata
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/instancedata/instancedatabymeterids

        [Route("api/instancedata/instancedatabymeterids")]
        public IQueryable<instancedata> PostInstanceDataByMeterids([FromBody]MeterList meterList)
        {
            var meterids = meterList.MeterString.Split(',');

            List<long> meteridsList = new List<long>();

            foreach (var meterid in meterids)
            {
                meteridsList.Add(Convert.ToInt64(meterid));
            }

            return instanceDataService.GetInstanceDataByMeterIds(meteridsList);
        }

        [Route("api/instancedata/instancedatadynamicbymeteridsforspecificperiod/{tablename}/{columnname}")]
        public IQueryable<instancedata> PostInstanceDataDynamicByMeteridsForSpecificPeriod(string tablename, string columnname, [FromBody]MeterList meterList)
        {
            var meterids = meterList.MeterString.Split(',');

            List<long> meteridsList = new List<long>();

            foreach (var meterid in meterids)
            {
                meteridsList.Add(Convert.ToInt64(meterid));
            }

            string fromDateTime = string.Empty;
            string toDateTime = string.Empty;
            if (meterList.FromSelectionFilter != "" && meterList.FromSelectionFilter != null
                    && meterList.ToSelectionFilter != "" && meterList.ToSelectionFilter != null
                    && meterList.FromTime != "" && meterList.FromTime != null
                    && meterList.ToTime != "" && meterList.ToTime != null
                    )
            {

                fromDateTime = GetBlockInformation.GetDateCondition(meterList.FromSelectionFilter, meterList.FromTime);
                toDateTime = GetBlockInformation.GetDateCondition(meterList.ToSelectionFilter, meterList.ToTime);

            }


            return instanceDataService.GetInstanceDynamicDataByMeterIdsForSpecificPeriod(
                tablename, columnname, meteridsList, fromDateTime, toDateTime);
        }

        // GET api/instancedata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/instancedata
        public void Post([FromBody]string value)
        {
        }

        // PUT api/instancedata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/instancedata/5
        public void Delete(int id)
        {
        }
    }  


}
