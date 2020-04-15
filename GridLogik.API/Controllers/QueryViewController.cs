using Domain.Common;
using Domain.Services;
using GridLogik.ViewModels;
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
    public class QueryViewController : ApiController
    {
        ISchemaService schemaService;
        IPrmGlobalService prmGlobalService;
        public QueryViewController(ISchemaService schemaService,
            IPrmGlobalService prmGlobalService)
        {
            this.schemaService = schemaService;
            this.prmGlobalService = prmGlobalService;
        }
        // GET api/queryview
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/queryview/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/queryview
        public IHttpActionResult Post([FromBody]QueryViewModel queryViewModel)
        {
            var reportDisplayDate = prmGlobalService.FindBy(x => x.prmunit == "REPORT_DISPLAY" && x.prmmodule == "Global")
                 .Select(x => x.prmvalue).FirstOrDefault();

            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;
            DataTable historydata = schemaService.GetQueryView(queryViewModel, FormatCharacter.InformixToCharFormat(reportDisplayDate), dateformat);
            //return CreatedAtRoute("", new { id = 1 }, historydata);
            return Ok(historydata);
        }

        // PUT api/queryview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/queryview/5
        public void Delete(int id)
        {
        }
    }
}
