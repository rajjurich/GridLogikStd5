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
    public class ExceptionController : ApiController
    {
        IExceptionService _exceptionService;
        public ExceptionController(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
        }
        // GET api/exception
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/exception/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/exception
        public async Task<IHttpActionResult> Post([FromBody]msterrorlog _msterrorlog)
        {
            await _exceptionService.Add(_msterrorlog);
            return Ok();
        }

        // PUT api/exception/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/exception/5
        public void Delete(int id)
        {
        }
    }
}
