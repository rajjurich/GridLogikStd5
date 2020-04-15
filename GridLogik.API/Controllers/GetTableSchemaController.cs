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
    public class GetTableSchemaController : ApiController
    {
        ISchemaService schemaService;
        public GetTableSchemaController(ISchemaService schemaService)
        {
            this.schemaService = schemaService;
        }
        // GET api/gettableschema
        [Route("api/gettableschema/{Module}")]
        public IQueryable<TblData> Get(string Module)
        {
            IQueryable<TblData> t = schemaService.GetSchemas(Module);

            return t;
        }

        // GET api/gettableschema/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/gettableschema
        public void Post([FromBody]string value)
        {
        }

        // PUT api/gettableschema/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/gettableschema/5
        public void Delete(int id)
        {
        }
    }
}
