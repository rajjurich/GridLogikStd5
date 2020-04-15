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
    public class ParameterMFController : ApiController
    {
        public IParameterMFService _parameterMFService { get; set; }
        public ParameterMFController(IParameterMFService parameterMFService)
        {
            _parameterMFService = parameterMFService;

        }
        // GET api/parametermf
        public IQueryable<parametermf> Get()
        {
            return _parameterMFService.GetAll();
        }

        // GET api/parametermfwithvalues
        [Route("api/parametermf/getparametermfwithvaluesbymeterid/{id}")]
        public IQueryable<parametermf> GetParameterMFWithValuesByMeterId(int id)
        {
            return _parameterMFService.GetParameterMFWithValuesByMeterId(id);
        }

        // GET api/parametermf/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var parametermf = await _parameterMFService.Get(id);

            return Ok(parametermf);
        }

        // POST api/parametermf
        public void Post([FromBody]string value)
        {
        }

        // PUT api/parametermf/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/parametermf/5
        public void Delete(int id)
        {
        }
    }
}
