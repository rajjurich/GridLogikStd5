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
    public class ProfileLogController : ApiController
    {
        ILoadSurveyService iLoadSurveyService;
        public ProfileLogController(ILoadSurveyService iLoadSurveyService)
        {
            this.iLoadSurveyService = iLoadSurveyService;
        }
        // GET api/profilelog
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/profilelog/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/profilelog
        public List<LoadService> Post([FromBody]ProfileLogViewModel model)
        {
            List<LoadService> objLoad = new List<LoadService>();
            objLoad = iLoadSurveyService.GetProfileLog(model);
            return objLoad;
        }

        // PUT api/profilelog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/profilelog/5
        public void Delete(int id)
        {
        }
    }
}
