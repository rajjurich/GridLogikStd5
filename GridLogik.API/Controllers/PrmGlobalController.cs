using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

namespace GridLogik.API.Controllers
{
    public class PrmGlobalController : ApiController
    {
        IPrmGlobalService prmGlobalService;
        public PrmGlobalController(IPrmGlobalService prmGlobalService)
        {
            this.prmGlobalService = prmGlobalService;
        }
        // GET api/prmglobal
        public IQueryable<prmglobal> Get()
        {
            return prmGlobalService.GetAll();
        }

        // GET api/prmglobal/getdateformat
        [Route("api/prmglobal/getdateformat")]
        public IQueryable<prmglobal> GetDateFormat()
        {
            var prmglobal = prmGlobalService.FindBy(prm => (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "DATEFIELD" && prm.prmidentifier.ToUpper() == "FORMAT") ||
                                        (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "SERVERDATE" && prm.prmidentifier.ToUpper() == "FORMAT") ||
                                        (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "DATABASEDATE" && prm.prmidentifier.ToUpper() == "FORMAT") ||
                                           (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "DATETIMEFIELD" && prm.prmidentifier.ToUpper() == "FORMAT") ||
                                           (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "CURRENTDATETIMEFIELD" && prm.prmidentifier.ToUpper() == "FORMAT")
                                           || (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "REPORT_DISPLAY" && prm.prmidentifier.ToUpper() == "FORMAT"));
            return prmglobal;
        }

        // GET api/prmglobal/GetIdentifiersOnModule/getdateformat
        [Route("api/prmglobal/GetIdentifiersOnModule/{module}")]
        public IQueryable<prmglobal> GetIdentifiersOnModule(string module)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "global" || prm.prmmodule.ToLower() == module.ToLower());
            return prmglobal;
        }


        // GET api/prmglobal/GetIdentifiersOnModuleNew/getdateformat
        [Route("api/prmglobal/GetIdentifiersOnModuleNew/{module}")]
        public IQueryable<prmglobal> GetIdentifiersOnModuleNew(string module)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == module.ToLower());
            return prmglobal;
        }

        // GET api/prmglobal/GetIdentifiersOnModuleNew/getdateformat
        [Route("api/prmglobal/GetIdentifiersOnModuleAndUnit/{module}/{unit}")]
        public IQueryable<prmglobal> GetIdentifiersOnModuleAndUnit(string module, string unit)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == module.ToLower() && prm.prmunit.ToLower() == unit.ToLower());
            return prmglobal;
        }

        // GET api/prmglobal/GetMetersByQueryViewValue/tableName
        [Route("api/prmglobal/GetMetersByQueryViewValue/{tableName}")]
        public IQueryable<prmglobal> GetMetersByQueryViewValue(string tableName)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == "queryview" && prm.prmvalue == tableName);
            return prmglobal;
        }


        // GET api/prmglobal/GetIdentifiersOnModule/getdateformat
        [Route("api/prmglobal/GetValuesOnModuleUnitAndIdentifier/{module}/{unit}/{identifier}")]
        public IQueryable<prmglobal> GetValuesOnModuleUnitAndIdentifier(string module, string unit, string identifier)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmmodule.ToLower() == module.ToUpper() || prm.prmunit.ToLower() == unit.ToLower() || prm.prmidentifier.ToLower() == identifier.ToLower());
            return prmglobal;
        }

        // GET api/prmglobal/GetIdentifiersOnModule/getdateformat
        [Route("api/prmglobal/GetIdentifiersOnUnit/{unit}")]
        public IQueryable<prmglobal> GetIdentifiersOnUnit(string unit)
        {
            var prmglobal = prmGlobalService.FindBy(prm => prm.prmunit.ToLower() == unit.ToLower());
            return prmglobal;
        }

        // GET api/prmglobal/5
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await prmGlobalService.Get(id));
        }

        // POST api/prmglobal
        public async Task<IHttpActionResult> Post([FromBody]prmglobal _prmglobal)
        {
            var prmglobal = await prmGlobalService.Add(_prmglobal);

            return CreatedAtRoute("DefaultApi", new { id = prmglobal.prmrecid }, prmglobal);
        }

        // PUT api/prmglobal/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]prmglobal _prmglobal)
        {
            var prmglobal = await prmGlobalService.Edit(_prmglobal);
            return Ok(prmglobal);


        }

        // DELETE api/prmglobal/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var _prmglobal = await prmGlobalService.Get(id);
            var prmglobal = await prmGlobalService.Remove(_prmglobal);
            return Ok(prmglobal);
        }
    }
}
