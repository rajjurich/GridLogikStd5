using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Configuration;
using Newtonsoft.Json;
using GridLogik.ViewModels;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using GridLogikViewer.Filters;
using System.Net.Http;
using System.Threading.Tasks;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class MeterLogController : Controller
    {

        //string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        [AccessCheck(IdParamName = "MeterLog/Index")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";

            IEnumerable<HTAlarm> htalarmobj;
            HTAlarm obj = new HTAlarm();
            obj.onoff = "1";
            obj.fltrFromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            obj.fltrToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            using(HttpClient client=new HttpClient())
            {
                uri = string.Format("{0}MeterLog/meterfilter", _uri);

                var result = await client.PostAsJsonAsync(uri,obj);

                htalarmobj = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
            }

            return View(htalarmobj);
        }

        //[Route("MeterLog/lang")]
        public async Task<ActionResult> Niulog()
        {

          IEnumerable<HTAlarm> htalarmobj;
          HTAlarm obj = new HTAlarm();
          obj.onoff = "1";
          obj.fltrFromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
          obj.fltrToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
          using (HttpClient client = new HttpClient())
          {
              uri = string.Format("{0}MeterLog/NiuLogAPIFilter", _uri);

              var result = await client.PostAsJsonAsync(uri,obj);

              htalarmobj = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
              TempData["MeterLogList"] = htalarmobj;
          }

          return View("Niulog", htalarmobj);
            
        }




        public async Task<ActionResult> Export(HTAlarm obj)//HTAlarm model
        {
            IEnumerable<HTAlarm> meterlog;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterLog/meterfilter", _uri);
                var result = await client.PostAsJsonAsync(uri, obj);
                meterlog = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
            }

            //List<HTAlarm> meterlog = new List<HTAlarm>();
            //using (WebClient web = new WebClient())
            //{
            //    string s = web.DownloadString(url + "MeterLogAPI/meterfilter/" + JsonConvert.SerializeObject(obj));
            //    meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);

            //    TempData["MeterLogList"] = meterlog;
            //}
            //string Jsonstr;
            //string Message;
            //string MessageType;

            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add("Content-Type", "application/json");
            //    Jsonstr = client.UploadString(url + "MeterLogAPI/meterfilter", "POST", JsonConvert.SerializeObject(obj));
            //    dynamic dynamicdata = JValue.Parse(Jsonstr);

            //    meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(Jsonstr);

            //}

            var reducedList = meterlog.Select(e => new {  e.ID, e.metername, e.location,e.starttimelog,e.stoptimelog}).ToList();
            //TempData["MeterLogList"] = reducedList;
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

       [HttpGet]
        public async Task<int> ExportNiu(HTAlarm obj)
        {
            IEnumerable<HTAlarm> meterlog;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterLog/NiuLogAPIFilter", _uri);
                var result = await client.PostAsJsonAsync(uri, obj);
                meterlog = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
                TempData["MeterLogList"] = meterlog;
            }
           // string Jsonstr;
           //// string Message;
           //// string MessageType;
           //List<HTAlarm> meterlog = new List<HTAlarm>();
           //using (WebClient client = new WebClient())
           //{
           //    client.Headers.Add("Content-Type", "application/json");
           //    Jsonstr = client.UploadString(url + "MeterLogAPI/NiuLogAPIFilter", "POST", JsonConvert.SerializeObject(obj));
           //    dynamic dynamicdata = JValue.Parse(Jsonstr);

           //    meterlog = JsonConvert.DeserializeObject<List<HTAlarm>>(Jsonstr);
           //    TempData["MeterLogList"] = meterlog;
           //}
           var reducedList = meterlog.Select(e => new { e.alarmid, e.converterip, e.starttimelog,e.stoptimelog}).ToList();
            var grid = new GridView();
           // var data = TempData["MeterLogList"];

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
            //TempData["MeterLogList"] = data;
            Response.End();
            return 1;
        }
	}
}