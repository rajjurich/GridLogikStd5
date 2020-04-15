using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class EnergyLogController : ApiController
    {
        ILoadSurveyService iLoadSurveyService;
        public EnergyLogController(ILoadSurveyService iLoadSurveyService)
        {
            this.iLoadSurveyService = iLoadSurveyService;
        }
        // GET api/energylog
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/energylog/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/energylog
        public List<LoadService> Post([FromBody]EnergyLogViewModel model)
        {
            List<LoadService> objLoad = new List<LoadService>();
            objLoad = iLoadSurveyService.GetEnergyLog(model);
            return objLoad;
        }

        // PUT api/energylog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/energylog/5
        public void Delete(int id)
        {
        }
    }
}
