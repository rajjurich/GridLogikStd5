using GridLogik.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class EventController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AlarmLog()
        {
            List<HTAlarm> alarmlog = new List<HTAlarm>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "AlarmLogAPI/" + Session["usrrecid"].ToString());
                alarmlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);
                TempData["AlarmLogList"] = alarmlog;
            }
            return View(alarmlog);
        }
        public ActionResult MeterLog()
        {
            List<HTAlarm> meterlog = new List<HTAlarm>();
            using (WebClient web = new WebClient())
            {
                string s = web.DownloadString(url + "MeterLogAPI/MeterLog/" + Session["usrrecid"].ToString());
                meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);
                TempData["MeterLogList"] = meterlog;
            }
            return View(meterlog);
        }
        public ActionResult NiuLog()
        {
            List<HTAlarm> meterlog = new List<HTAlarm>();
            using (WebClient web = new WebClient())
            {
                string s = web.DownloadString(url + "MeterLogAPI/Niulog/" + Session["usrrecid"].ToString());
                meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);

                TempData["MeterLogList"] = meterlog;
            }
            return View("Niulog", meterlog);
        }
        public void ExportAlarm(HTAlarm model)
        {
            List<HTAlarm> alarmlog = new List<HTAlarm>();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                string s = client.UploadString(url + "AlarmLogAPI/filter", "POSt", JsonConvert.SerializeObject(model));
                alarmlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);
                TempData["AlarmLogList"] = alarmlog;
            }

            var grid = new GridView();
            var reducedList = alarmlog.Select(e => new { e.ID, e.alarmname, e.alarmmessage, e.metername, e.starttimelog, e.stoptimelog }).ToList();
            grid.DataSource = reducedList;
            grid.DataBind();
            grid.HeaderRow.Cells[0].Text = "ID";
            grid.HeaderRow.Cells[1].Text = "Alarm Name";
            grid.HeaderRow.Cells[2].Text = "Alarm Message";
            grid.HeaderRow.Cells[3].Text = "Meter Name";
            grid.HeaderRow.Cells[4].Text = "Start Time";
            grid.HeaderRow.Cells[5].Text = "End Time";

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=alarmlog_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public ActionResult ExportMeter(HTAlarm model)
        {
            List<HTAlarm> meterlog = new List<HTAlarm>();
            string Jsonstr;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                Jsonstr = client.UploadString(url + "MeterLogAPI/meterfilter", "POST", JsonConvert.SerializeObject(model));
                dynamic dynamicdata = JValue.Parse(Jsonstr);
                meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(Jsonstr);
            }
            var reducedList = meterlog.Select(e => new { e.ID, e.metername, e.location, e.starttimelog, e.stoptimelog }).ToList();
            var grid = new GridView();
            grid.DataSource = reducedList;
            grid.DataBind();
            grid.HeaderRow.Cells[0].Text = "ID";
            grid.HeaderRow.Cells[1].Text = "Meter Name";
            grid.HeaderRow.Cells[2].Text = "Location";
            grid.HeaderRow.Cells[3].Text = "Start Time";
            grid.HeaderRow.Cells[4].Text = "Stop Time";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=MeterLog_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));//
            grid.Dispose();
            htw.Dispose();
            return File(temp, "application/vnd.ms-excel");
        }
        public int ExportNiu(HTAlarm model)
        {
            string Jsonstr;
            // string Message;
            // string MessageType;
            List<HTAlarm> meterlog = new List<HTAlarm>();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                Jsonstr = client.UploadString(url + "MeterLogAPI/NiuLogAPIFilter", "POST", JsonConvert.SerializeObject(model));
                dynamic dynamicdata = JValue.Parse(Jsonstr);

                meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(Jsonstr);
                TempData["MeterLogList"] = meterlog;
            }
            var reducedList = meterlog.Select(e => new { e.alarmid, e.converterip, e.starttimelog, e.stoptimelog }).ToList();
            var grid = new GridView();
            grid.DataSource = reducedList;
            grid.DataBind();
            grid.HeaderRow.Cells[0].Text = "Alarm Id";
            grid.HeaderRow.Cells[1].Text = "Converter IP";
            grid.HeaderRow.Cells[2].Text = "Start Time";
            grid.HeaderRow.Cells[3].Text = "Stop Time";
            //grid.HeaderRow.Cells[8].Visible = false;
            //grid.HeaderRow.Cells[9].Visible = false;
            int a = grid.Columns.Count;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Niulog" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls");

            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            return 1;
        }
	}
}