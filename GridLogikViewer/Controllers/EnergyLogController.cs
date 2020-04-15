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
using System.Globalization;
using System.Data;
using CMS.Framework.Common;
using System.Net.Http;
using System.Threading.Tasks;
using GridLogik.ViewModels;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class EnergyLogController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        private List<string> TimeSlotList()
        {
            var list = new List<string>()
            {
                "12:00 AM","12:15 AM","12:30 AM","12:45 AM","01:00 AM","01:15 AM","01:30 AM","01:45 AM","02:00 AM","02:15 AM",
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
        private async Task<List<MeterModel>> ListMeterModel()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            IEnumerable<MeterModel> meterModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel", _uri);

                var result = await client.GetAsync(uri);

                meterModels = await result.Content.ReadAsAsync<IEnumerable<MeterModel>>();

            }
            meterModel = meterModels.ToList();
            return meterModel;
        }

        private async Task<List<MeterGroup>> ListMeterGroup()
        {
            List<MeterGroup> meterGroup = new List<MeterGroup>();
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            meterGroup = meterGroups.ToList();
            return meterGroup;
        }

        private List<PropertyInfo> LoadServiceList()
        {
            LoadService loadService = new LoadService();
            System.Reflection.PropertyInfo[] array = loadService.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 4);
            list.RemoveRange(26, 22);
            return list;
        }
        // GET: TrendsData
        public async Task<ActionResult> Index()
        {
            EnergyLogViewModel model = new EnergyLogViewModel();
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            model.Groups = await ListMeterGroup();
            model.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            model.ToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            model.LoadServiceList = new List<LoadService>();
            ViewBag.RecordCount = "false";
            return View("EnergyLog", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(EnergyLogViewModel model)
        {

            DataTable obj = new DataTable();
            Boolean endDate = false;
            if (model.StartTime == "select" || model.StartTime == null)
                model.StartTime = "12:00 AM";

            if (model.FromDate != null)
            {
                model.fltrFromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddMinutes(-15);

                if (model.EndTime == "select" || model.EndTime == null)
                {
                    endDate = true;
                    model.EndTime = "12:00 AM";
                    //model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
                    model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1).AddMinutes(15);
                }
                else
                {
                    model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddMinutes(15);
                }
            }
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            model.Groups = (await ListMeterGroup()).ToList();
            List<LoadService> loadServiceList = new List<LoadService>();
            using (WebClient client = new WebClient())
            {
                var s = new JsonResult();
                client.Headers.Add("Content-Type", "application/json");
                string s1 = client.UploadString(url + "EnergyLog", JsonConvert.SerializeObject(model));
                if (s1 != null)
                {



                    model.LoadServiceList = JsonConvert.DeserializeObject<List<LoadService>>(s1);

                    obj = JsonConvert.DeserializeObject<DataTable>(s1);

                    foreach (DataColumn column in obj.Columns)
                        column.ColumnName = column.ColumnName.ToUpper();
                    obj.AcceptChanges();



                    if (model.LoadServiceList.Count == 0)
                        ViewBag.RecordCount = "false";
                    // TempData["LoadServiceList"] = model.LoadServiceList;
                    //TempData["LoadServiceList"] = obj;
                    TempData["LoadServiceList"] = model.LoadServiceList;
                    model.LoadServiceColumn = LoadServiceList();
                    ViewBag.RecordCount = "true";
                }

            }
            if (model.FromDate != null)
            {
                model.fltrFromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                if (endDate)
                {
                    model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1);
                    model.ToDate = model.fltrToDate.ToString("MM/dd/yyyy").Replace('-', '/');
                }
                else
                {
                    if (model.EndTime == "select" || model.EndTime == "12:00 AM")
                    {
                        model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        model.fltrToDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    }
                }
            }
            return View("EnergyLog", model);
        }


        public void Export(string id, string exportAllColumns)
        {
            string[] selectedcol = id.Split(',');
            var grid = new GridView();
            var data = TempData["LoadServiceList"];
            //-------------
            List<LoadService> lst = new List<LoadService>();
            List<GridLogik.ViewModels.LoadService> lstrefined = new List<LoadService>();
            lst = (List<GridLogik.ViewModels.LoadService>)TempData["LoadServiceList"];

            // var r= lst.Select(e => new {e.CBlk_Avg_Hz, e.CBlk_Avg_MW,e.Date}).ToList();
            if (exportAllColumns != "Y")
            {
                var r = lst.Select(e => clsCommon.getProperties(selectedcol, e)).ToList();
                DataTable dt = new DataTable();
                dt = clsCommon.ToDataTable(r);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToString().ToUpper();
                }
                grid.DataSource = dt; ;
                grid.DataBind();
            }
            else
            {
                grid.DataSource = lst;
                grid.DataBind();
            }
            //-------------




            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=energylog_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");

            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            TempData["LoadServiceList"] = data;
            Response.End();
        }
    }
}