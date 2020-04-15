using Domain.Common;
using Domain.Entities;
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
    public class MeterViewController : ApiController
    {
        IInstanceDataService iInstanceDataService;
        ILoadSurveyService iLoadSurveyService;
        IPrmGlobalService prmGlobalService;
        public MeterViewController(IInstanceDataService iInstanceDataService
            , ILoadSurveyService iLoadSurveyService
            , IPrmGlobalService prmGlobalService)
        {
            this.iInstanceDataService = iInstanceDataService;
            this.iLoadSurveyService = iLoadSurveyService;
            this.prmGlobalService = prmGlobalService;
        }
        // GET api/meterview/GetInstancedata/{MeterID}
        [Route("api/MeterView/GetInstancedata/{MeterID}")]
        public async Task<IHttpActionResult> GetInstancedata(int meterid)
        {
            var data = await iInstanceDataService.Get(meterid);
            return Ok(data);
        }

        // POST api/meterview/QueryData
        [HttpPost]
        [Route("api/MeterView/QueryData")]
        public List<LoadService> QueryData(clsMeterviewModel groupdisplayquerydata)
        {
            List<LoadService> objLoad = new List<LoadService>();

            string condition = string.Empty;
            if (groupdisplayquerydata.FromDate.Contains('-'))
            {
                groupdisplayquerydata.FromDate = groupdisplayquerydata.FromDate.Replace('-', '/');
            }
            if (groupdisplayquerydata.ToDate.Contains('-'))
            {
                groupdisplayquerydata.ToDate = groupdisplayquerydata.ToDate.Replace('-', '/');
            }

            if (groupdisplayquerydata.Param == "E")
            {
                objLoad = iLoadSurveyService.GetMeterviewenergy(groupdisplayquerydata, condition);
            }
            else if (groupdisplayquerydata.Param == "P")
            {
                objLoad = iLoadSurveyService.GetMeterviewprofile(groupdisplayquerydata, condition);
            }
            else if (groupdisplayquerydata.Param == "C")
            {
                objLoad = iLoadSurveyService.GetMeterviewconsumption(groupdisplayquerydata, condition);
            }
            else if (groupdisplayquerydata.Param == "CI" || groupdisplayquerydata.Param == "CE")
            {
                objLoad = iLoadSurveyService.GetMeterviewconsumptionExportImport(groupdisplayquerydata, condition);
            }

            return objLoad;
        }

        [HttpPost]
        [Route("api/MeterView/CommonView")]
        public async Task<IHttpActionResult> CommonView(CommonView groupdisplayquerydata)
        {
            // List<LoadService> objLoad = new List<LoadService>();
            DataTable dt = new DataTable();
            var reportDisplayDate = prmGlobalService.FindBy(x => x.prmunit == "REPORT_DISPLAY" && x.prmmodule == "Global")
                 .Select(x => x.prmvalue).FirstOrDefault();
            var dateformat = prmGlobalService.FindBy(x => x.prmunit == "DateFieldcs").Select(x => x.prmvalue).FirstOrDefault();
            dateformat = dateformat == null ? "dd/MM/yyyy hh:mm tt" : dateformat;

            dt = await iLoadSurveyService.GetCommonData(groupdisplayquerydata, FormatCharacter.InformixToCharFormat(reportDisplayDate), dateformat);
            return Ok(dt);

        }


        // GET api/meterview
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/meterview/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/meterview
        public void Post([FromBody]string value)
        {
        }

        // PUT api/meterview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/meterview/5
        public void Delete(int id)
        {
        }
    }
}
