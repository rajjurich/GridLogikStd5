using Domain.Common;
using Domain.Entities;
using Domain.Services;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GridLogik.API.Controllers
{
    public class HistoryController : ApiController
    {
        IHistoryService _Historyservice;
        IPrmGlobalService prmGlobalService;
        IMeterService meterService;
        StringBuilder sb = new StringBuilder();
        etools_devEntities db;        
        public HistoryController(IHistoryService Historyservice
            , IPrmGlobalService prmGlobalService
            , IMeterService meterService
            , DbContext db)
        {
            _Historyservice = Historyservice;
            this.prmGlobalService = prmGlobalService;
            this.meterService = meterService;
            this.db = (etools_devEntities)db;
        }

        public IQueryable<loadsurveylog> Get()
        {
            return _Historyservice.GetAll();
        }

        [HttpPost]
        [Route("api/History/PostData")]
        public IHttpActionResult PostData(HistoryModel historymodel)
        {
            DataTable dt = new DataTable();
            long MeterGroup = Convert.ToInt32(historymodel.MeterGroupId);

            var metersidsstring = meterService.GetMetersByGroupID(MeterGroup.ToString());// _Historyservice.GetMeterByGroupId(MeterGroup);

            string meteridlist = string.Join(",", metersidsstring.Select(x => x.ID.ToString()).ToArray());

            var reportDisplayDate = prmGlobalService.FindBy(x => x.prmunit == "REPORT_DISPLAY" && x.prmmodule == "Global")
                 .Select(x => x.prmvalue).FirstOrDefault();

            var RptDispalyFormat = FormatCharacter.InformixToCharFormat(reportDisplayDate);

            var prmvalue = prmGlobalService.FindBy(x => x.prmrecid == historymodel.Queryid).Select(x => x.prmvalue).SingleOrDefault();

            StringBuilder sb = new StringBuilder();

            sb.Append("select TO_CHAR(tstamp::datetime year to second,'" + RptDispalyFormat + "') as  Date_Time, tstamp,blockno");
            foreach (var mtr in metersidsstring)
            {
                sb.Append("," + "MAX(CASE WHEN meterid=" + "'" + mtr.ID + "'" + " THEN round(" + prmvalue + ",4) END) as " + mtr.MeterName.Replace(" ", ""));                
            }
            if (historymodel.Interval.ToString().ToLower() == "b")
            {
                sb.Append(" from loadsurveylogs where meterid in (" + meteridlist + ")and (tstamp>" + "'" + historymodel.fltrFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and tstamp<" + "'" + historymodel.fltrToDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ") group by tstamp,blockno order by tstamp");
            }
            else if(historymodel.Interval.ToString().ToLower() == "h")
            {
                sb.Append(" from loadsurveylogs where mod(blockno,4)=0 and meterid in (" + meteridlist + ")and (tstamp>" + "'" + historymodel.fltrFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and tstamp<" + "'" + historymodel.fltrToDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ") group by tstamp,blockno order by tstamp");
            }
                 else if(historymodel.Interval.ToString().ToLower() == "d")
            {
                sb.Append(" from loadsurveylogs where mod(blockno,96)=0 and meterid in (" + meteridlist + ")and (tstamp>" + "'" + historymodel.fltrFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and tstamp<" + "'" + historymodel.fltrToDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ") group by tstamp,blockno order by tstamp");
            }
            else
            {
                sb.Append(" from loadsurveylogs where meterid in (" + meteridlist + ")and (tstamp>" + "'" + historymodel.fltrFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and tstamp<" + "'" + historymodel.fltrToDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ") group by tstamp,blockno order by tstamp");
            }

            var query = sb.ToString();


            var conn = db.Database.Connection;
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }
            if (dt.Columns.Contains("tstamp"))
            {
                dt.Columns.Remove("tstamp");
                dt.AcceptChanges();
            }
            if (dt.Columns.Contains("date_Time"))
            {
                dt.Columns["date_Time"].ColumnName = "Date_Time";
                dt.AcceptChanges();
            }
            return Ok(dt);

        }


        [HttpPost]
        // POST api/loadsurveylog
        public async Task<IHttpActionResult> Post([FromBody]loadsurveylog _loadsurveylog)
        {

            var loadsurveylog1 = await _Historyservice.Add(_loadsurveylog);

            return CreatedAtRoute("DefaultApi", new { id = loadsurveylog1.meterid }, loadsurveylog1);
        }

        // PUT api/loadsurveylog/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]loadsurveylog _loadsurveylog)
        {
            if (id != _loadsurveylog.meterid)
            {
                throw new Exception("Invalid History Model");
            }
            var loadsurveylog1 = await _Historyservice.Edit(_loadsurveylog);

            return Ok(loadsurveylog1);
        }


        // DELETE api/loadsurveylog/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var loadsurveylog1 = await _Historyservice.Get(id);
            if (loadsurveylog1 == null)
            {
                throw new Exception("Invalid History Model");
            }
            var loadsrvey = await _Historyservice.Remove(loadsurveylog1);
            return Ok(loadsrvey);
        }


    }
}
