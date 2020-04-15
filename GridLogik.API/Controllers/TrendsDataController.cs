using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class TrendsDataController : ApiController
    {
        ITrendsDataService trendsDataService;
        public TrendsDataController(ITrendsDataService trendsDataService)
        {
            this.trendsDataService = trendsDataService;
        }
        // GET api/trendsdata
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/trendsdata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/trendsdata
        public List<InstanceDataLog> Post([FromBody]TrendsDataViewModel model)
        {
            List<InstanceDataLog> data = new List<InstanceDataLog>();
            if (model.RadioFilter == false)
            {
                data = trendsDataService.GetTrendsData(model);
            }
            else
            {
                data = trendsDataService.GetTrendsDataLog(model);
            }
            return data;
        }

        // PUT api/trendsdata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/trendsdata/5
        public void Delete(int id)
        {
        }
    }
}
