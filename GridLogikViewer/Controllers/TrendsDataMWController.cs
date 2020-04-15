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
using GridLogik.ViewModels;
using System.Web.Mvc.Html;
using System.Net.Mime;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Globalization;
using System.Dynamic;
using System.Data;
using CMS.Framework.Common;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class TrendsDataMWController : Controller
    {
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
        private List<MeterModel> ListMeterModel()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            return meterModel;
        }

        private List<MeterGroup> ListMeterGroup()
        {
            List<MeterGroup> meterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterGroupAPI");
                meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }
            return meterGroup;
        }

        private List<PropertyInfo> InstaceDataList()
        {
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 6);
            list.RemoveRange(1, 2);
            list.RemoveRange(18, 59);
            return list;
        }

        // GET: TrendsData
        public ActionResult Index()
        {
            TrendsDataViewModel model = new TrendsDataViewModel();
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            //model.FromDate = DateTime.ParseExact(DateTime.Now.ToShortDateString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.InstanceDataList = new List<InstanceDataLog>();
            model.InstanceDataColumn = InstaceDataList();
            model.InstanceDataLogSecondsList = new List<InstanceDataLog>();
            ViewBag.RecordCount = "false";
            return View("TrendsDataMW", model);
        }

        [HttpPost]
        public ActionResult Update(TrendsDataViewModel model)
        {
            if (model.StartTime == "select" || model.StartTime == null)
                model.StartTime = "12:00 AM";
            if (model.FromDate != null)
            {
                model.fltrFromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddSeconds(-1);

                if (model.EndTime == "select" || model.EndTime == null)
                {
                    model.EndTime = "12:00 AM";
                    model.fltrToDate = DateTime.ParseExact(model.FromDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(1);
                }
                else
                {
                    model.fltrToDate = DateTime.ParseExact(model.FromDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddSeconds(1);
                }
            }

            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            // model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            List<InstanceDataLog> instanceDataList = new List<InstanceDataLog>();
            model.InstanceDataList = new List<InstanceDataLog>();
            model.InstanceDataLogSecondsList = new List<InstanceDataLog>();
            var selectedColumns = model.SelectedColumn;
            model.SelectedColumn = null;
            using (WebClient client = new WebClient())
            {
                var s = new JsonResult();
                client.Headers.Add("Content-Type", "application/json");
                string s1 = client.UploadString(url + "TrendsDataAPI", JsonConvert.SerializeObject(model));
                if (model.RadioFilter == false)
                {
                    if (s1 != null)
                    {
                        model.InstanceDataList = JsonConvert.DeserializeObject<List<InstanceDataLog>>(s1);
                        TempData["InstanceDataList"] = model.InstanceDataList;
                        ViewBag.RecordCount = "true";
                    }
                }
                else
                {
                    if (s1 != null)
                    {
                        model.InstanceDataList = JsonConvert.DeserializeObject<List<InstanceDataLog>>(s1);
                        TempData["InstanceDataList"] = model.InstanceDataList;
                        ViewBag.RecordCount = "true";
                    }
                }
            }

            if (model.FromDate != null)
            {
                model.fltrFromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                if (model.EndTime == "select" || model.EndTime == "12:00 AM")
                {
                    model.fltrToDate = DateTime.ParseExact(model.FromDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1);
                }
                else
                {
                    model.fltrToDate = DateTime.ParseExact(model.FromDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                }
            }

            model.InstanceDataColumn = InstaceDataList();

            return View("TrendsDataMW", model);
        }

        public string replaceExcelCol(string str, string ColList)
        {
            string[] selectedcol = ColList.Replace("-", "_").Split(',');
            // ["vrn", "vyn", "vbn", "vln", "vry", "vyb", "vbr", "vll", "i", "ir", "iy", "ib", "pf", "kw", "kva", "kvar", "hz"];
            str = str.ToLower().Replace("vrn", selectedcol[0]);
            str = str.Replace("vyn", selectedcol[1]);
            str = str.Replace("vbn", selectedcol[2]);
            str = str.Replace("vln", selectedcol[3]);
            str = str.Replace("vry", selectedcol[4]);
            str = str.Replace("vyb", selectedcol[5]);
            str = str.Replace("vbr", selectedcol[6]);
            str = str.Replace("vll", selectedcol[7]);
            str = str.Replace("i", selectedcol[8]);
            str = str.Replace("ir", selectedcol[9]);
            str = str.Replace("iy", selectedcol[10]);
            str = str.Replace("ib", selectedcol[11]);
            str = str.Replace("pf", selectedcol[12]);
            str = str.Replace("kw", "mw");
            str = str.Replace("kva", "mva");
            str = str.Replace("kvar", "mvar");
            str = str.Replace("hz", selectedcol[16]);
            return str;
        }

        public void Export(string id, string exportAllColumns, string ColList)
        {
            string[] selectedcol = id.Replace("M", "K").Split(',');
            var grid = new GridView();
            var data = TempData["InstanceDataList"];
            List<InstanceDataLog> lst = new List<InstanceDataLog>();
            lst = (List<InstanceDataLog>)TempData["InstanceDataList"];

            if (exportAllColumns != "Y")
            {
                var r = lst.Select(e => clsCommon.getProperties(selectedcol, e)).ToList();
                grid.DataSource = clsCommon.ToDataTable(r); ;
                grid.DataBind();
            }
            else
            {
                grid.DataSource = lst;
                grid.DataBind();
            }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Trendsdata_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");

            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            string str1 = replaceExcelCol(sw.ToString(), ColList);

            Response.Write(str1);
            TempData["InstanceDataList"] = data;
            //var s = new JsonResult();
            //s.Data = new
            //{
            //    success = "true",
            //    data = Response
            //};
            Response.End();
            //  return s;
        }

        [HttpPost]
        public ActionResult MetersByGroupID(int id)
        {
            List<Meter> Meters = new List<Meter>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "meterAPI/GetMetersByGroupID" + "/" + id);
                Meters = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
            return Json(objMeters);
        }
    }
}