using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Mvc.Html;
using System.Net.Mime;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Globalization;
using GridLogik.ViewModels;
using System.Threading.Tasks;


namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"].ToString();
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        // GET: HistoryData
        public async Task<ActionResult> Index()
        {
            //var data = ViewData.Model as MstRoleMenuAccess;
            //if (data.rmacreateaccess == 0)
            //    ViewBag.CreateAccess = "False";
            //if (data.rmadeleteaccess == 0)
            //    ViewBag.DeleteAccess = "False";
            //if (data.rmaupdateaccess == 0)
            //    ViewBag.EditAccess = "False";

            HistoryModel history = new HistoryModel();
            history.StartTimeList = TimeSlotList();
            history.EndTimeList = TimeSlotList();
            history.QueryList = ListQuery();
            history.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            history.ToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            history.InstanceDataAverageLogList = new List<InstanceDataAverageLog>();
            await BindDropDown();
            return View(history);
        }


        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", url);
                var result = await client.GetAsync(uri);
                var MeterContent = await result.Content.ReadAsAsync<List<MeterGroup>>();
                var Metergroups = MeterContent.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.GroupName
                });
                ViewBag.Metergroups = Metergroups;
            }
        }

        private List<prmglobal> ListQuery()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal/GetIdentifiersOnModuleNew/HistoryData").Result;
            List<prmglobal> lstGlobal = null;
            if (response.IsSuccessStatusCode)
            {
                var objResponse = response.Content.ReadAsStringAsync().Result;
                lstGlobal = new List<prmglobal>();
                dynamic objPrmGlobal = JValue.Parse(objResponse);
                foreach (dynamic prm in objPrmGlobal)
                {
                    if (prm.prmmodule.ToString().ToLower() != "global")
                    {
                        prmglobal obj = new prmglobal();
                        obj.prmidentifier = prm.prmidentifier.ToString();
                        obj.prmmodule = prm.prmmodule.ToString();
                        obj.prmrecid = Convert.ToInt16(prm.prmrecid.ToString());
                        obj.prmunit = (prm.prmunit.ToString().IndexOf('.') > 0 ? prm.prmunit.ToString().Substring(prm.prmunit.ToString().IndexOf('.'), (prm.prmunit.ToString().Length) - (prm.prmunit.ToString().IndexOf('.'))).ToString().Replace(".", "") : prm.prmunit.ToString());
                        obj.prmvalue = prm.prmvalue.ToString();
                        obj.rfu1 = prm.rfu1.ToString();
                        obj.rfu2 = prm.rfu2.ToString();
                        lstGlobal.Add(obj);
                    }
                }
            }
            return lstGlobal;


        }


        private List<string> TimeSlotList()
        {
            var list = new List<string>()
            {
               
                "12:00 AM","12:15 AM","12:30 AM","12:45 AM","01:00 AM","01:15 AM","01:30 AM","01:45 AM","02:00 AM ","02:15 AM",
                "02:30 AM","02:45 AM","03:00 AM","03:15 AM","03:30 AM","03:45 AM","04:00 AM","04:15 AM","04:30 AM","04:45 AM",
                "05:00 AM","05:15 AM","05:30 AM","05:45 AM","06:00 AM","06:15 AM","06:30 AM","06:45 AM","07:00 AM","07:15 AM",
                "07:30 AM","07:45 AM","08:00 AM","08:15 AM","08:30 AM","08:45 AM","09:00 AM","09:15 AM","09:30 AM","09:45 AM",
                "10:00 AM","10:15 AM","10:30 AM","10:45 AM","11:00 AM","11:15 AM","11:30 AM","11:45 AM","12:00 PM","12:15 PM",
                "12:30 PM","12:45 PM","01:00 PM","01:15 PM","01:30 PM","01:45 PM","02:00 PM","02:15 PM","02:30 PM","02:45 PM",
                "03:00 PM","03:15 PM","03:30 PM","03:45 PM","04:00 PM","04:15 PM","04:30 PM","04:45 PM","05:00 PM","05:15 PM",
                "05:30 PM","05:45 PM","06:00 PM","06:15 PM","06:30 PM","06:45 PM","07:00 PM","07:15 PM","07:30 PM","07:45 PM",
                "08:00 PM","08:15 PM","08:30 PM","08:45 PM","09:00 PM","09:15 PM","09:30 PM","09:45 PM","10:00 PM","10:15 PM",
                "10:30 PM","10:45 PM","11:00 PM","11:15 PM","11:30 PM","11:45 PM"
            };
            return list;
        }
        //
        // GET: /History/
        //[HttpPost]
        public async Task<ActionResult> Create(HistoryModel histroy, FormCollection form)
        {
            string dtformat = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/GetIdentifiersOnUnit/DateFieldcs", _uri);
                var result = await client.GetAsync(uri);
                var content = await result.Content.ReadAsAsync<List<prmglobal>>();

                dtformat = content.Select(x => x.prmvalue).FirstOrDefault();


            }

            DataTable obj = new DataTable();
            DataTable excelDt = new DataTable();
            try
            {
                Boolean endDate = false;
                if (histroy.StartTime == "select" || histroy.StartTime == null)
                    histroy.StartTime = "12:00 AM";

                if (histroy.FromDate != null)
                {
                    histroy.fltrFromDate = DateTime.ParseExact(histroy.FromDate + " " + histroy.StartTime, dtformat, CultureInfo.InvariantCulture).AddMinutes(-15);

                    if (histroy.EndTime == "select" || histroy.EndTime == null)
                    {
                        endDate = true;
                        histroy.EndTime = "12:00 AM";
                        histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
                    }
                    else
                    {
                        histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture).AddMinutes(15);
                    }
                    //model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                }
                histroy.MeterGroupId = histroy.MeterGroupId;

                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}History/PostData", url);
                    var result = await client.PostAsJsonAsync(uri, histroy);
                    var contents = await result.Content.ReadAsStringAsync();

                    if (contents != null)
                    {

                        obj = JsonConvert.DeserializeObject<DataTable>(contents);

                        foreach (DataColumn column in obj.Columns)
                            column.ColumnName = column.ColumnName.ToUpper();
                        obj.AcceptChanges();

                        TempData["HistoryDataList"] = obj;
                        Session["HistoryDataList"] = obj;
                        ViewBag.HistoryData = obj.AsEnumerable();
                    }
                }
                if (histroy.FromDate != null)
                {
                    histroy.fltrFromDate = DateTime.ParseExact(histroy.FromDate + " " + histroy.StartTime, dtformat, CultureInfo.InvariantCulture);
                    histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture);

                }
                if (histroy.FromDate != null)
                {
                    histroy.fltrFromDate = DateTime.ParseExact(histroy.FromDate + " " + histroy.StartTime, dtformat, CultureInfo.InvariantCulture);

                    if (endDate)
                    {
                        histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture).AddDays(1);
                        histroy.ToDate = histroy.fltrToDate.ToString("MM/dd/yyyy").Replace('-', '/');
                    }
                    else
                    {
                        if (histroy.EndTime == "select" || histroy.EndTime == "12:00 AM")
                        {
                            histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            histroy.fltrToDate = DateTime.ParseExact(histroy.ToDate + " " + histroy.EndTime, dtformat, CultureInfo.InvariantCulture);
                        }
                    }

                }
                if (ViewBag.HistoryData != null)
                {

                    return View("_HistoryDataList", histroy);
                }
                return View("_HistoryDataList", histroy);


            }
            catch (Exception ex)
            {
                return View("_HistoryDataList", histroy);
            }


        }
        private List<PropertyInfo> InstaceDataAverageLogList()
        {
            InstanceDataAverageLog instaceDataAverageLog = new InstanceDataAverageLog();
            System.Reflection.PropertyInfo[] array = instaceDataAverageLog.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            return list;
        }


        //  public ActionResult Export()
        public void Export(HistoryModel model)
        {
            List<HistoryModel> historydatalist = new List<HistoryModel>();

            var grid = new GridView();
            var data = TempData["HistoryDataList"];
            grid.DataSource = TempData["HistoryDataList"];
            grid.DataBind();


            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=HistoryData_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");

            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            TempData["HistoryDataList"] = data;
            Response.End();


            //grid.DataSource = Session["HistoryDataList"];
            //grid.DataBind();

            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grid.RenderControl(htw);
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Daily_Report_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            //this.Response.ContentType = "application/vnd.ms-excel";
            //byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));
            //grid.Dispose();
            //htw.Dispose();
            //return File(temp, "application/vnd.ms-excel");

        }
    }
}