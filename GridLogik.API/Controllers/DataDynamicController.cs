using Domain.Model;
using Domain.Services;
using GridLogik.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class DataDynamicController : ApiController
    {
        IDataDynamicService dataDynamicService;
        public DataDynamicController(IDataDynamicService dataDynamicService)
        {
            this.dataDynamicService = dataDynamicService;
        }
        // GET api/datadynamic
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/datadynamic/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/datadynamic
        public void Post([FromBody]string value)
        {
        }


        [Route("api/datadynamic/{tablename}/{columnname}")]
        public async Task<IHttpActionResult> PostDataDynamic(string tablename, string columnname, [FromBody] MeterList meterList)
        {
            var data = await dataDynamicService.GetDataDynamic(tablename, columnname, meterList, null);
            return Ok(data);            
        }

        [Route("api/datadynamic/openquery/")]
        public async Task<DataTable> PostDataDynamicOpenQuery([FromBody] OpenQuery openQuery)
        {
            var data = await dataDynamicService.GetDataDynamicOpenQuery(openQuery.Query);
            return data;
        }
        // PUT api/datadynamic/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/datadynamic/5
        public void Delete(int id)
        {
        }
    }
}
