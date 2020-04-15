using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Net.Http;
using System.Threading.Tasks;
using GridLogikViewer.Filters;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class AlarmLogController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        
        //
        // GET: /AlarmLog/
         [AccessCheck(IdParamName = "AlarmLog/Index")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            //var data = ViewData.Model as MstRoleMenuAccess;
            //if (data.rmacreateaccess == 0)
            //    ViewBag.CreateAccess = "False";
            //if (data.rmadeleteaccess == 0)
            //    ViewBag.DeleteAccess = "False";
            //if (data.rmaupdateaccess == 0)
            //    ViewBag.EditAccess = "False";

            IEnumerable<HTAlarm> htalarmobj;
            HTAlarm obj = new HTAlarm();
            obj.fltrFromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            obj.fltrToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            obj.AllChecked = "All";
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}AlarmLog/PostAlarmLog", _uri);

                var result = await client.PostAsJsonAsync(uri, obj);

                htalarmobj = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();

                TempData["AlarmLogList"] = htalarmobj;
            }

            return View(htalarmobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(HTAlarm h)
        {
            IEnumerable<HTAlarm> htalarmobj;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}AlarmLog/filter", _uri);

                var result = await client.PostAsJsonAsync(uri, h);

                htalarmobj = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
                TempData["AlarmLogList"] = htalarmobj;
            }
            return View("Index", htalarmobj);
        }
        public async Task<ActionResult> Export(HTAlarm model)
        {
            

            //IEnumerable<HTAlarm> alarmlog;

            //using (HttpClient client = new HttpClient())
            //{
            //    url = string.Format("{0}AlarmLog/filter", _uri);
            //    var result = await client.PostAsJsonAsync(url, model);
            //    alarmlog = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
            //    TempData["AlarmLogList"] = alarmlog;
            //}


            ////using (WebClient client = new WebClient())
            ////{
            ////    client.Headers.Add("Content-Type", "application/json");
            ////    string s = client.UploadString(url + "AlarmLogAPI/filter", "POSt", JsonConvert.SerializeObject(model));
            ////    alarmlog = JsonConvert.DeserializeObject<List<HTAlarm>>(s);
            ////    TempData["AlarmLogList"] = alarmlog;
            ////}

            //var grid = new GridView();
            //var reducedList = alarmlog.Select(e => new { e.ID, e.alarmname, e.alarmmessage, e.metername, e.starttimelog, e.stoptimelog }).ToList();
            //grid.DataSource = reducedList;
            //grid.DataBind();
            //grid.HeaderRow.Cells[0].Text = "ID";
            //grid.HeaderRow.Cells[1].Text = "Alarm Name";
            //grid.HeaderRow.Cells[2].Text = "Alarm Message";
            //grid.HeaderRow.Cells[3].Text = "Meter Name";
            //grid.HeaderRow.Cells[4].Text = "Start Time";
            //grid.HeaderRow.Cells[5].Text = "End Time";
            
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename=alarmlog_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");
            //Response.ContentType = "application/excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grid.RenderControl(htw);
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=AlarmLog_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            //this.Response.ContentType = "application/vnd.ms-excel";
            //byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));//
            //grid.Dispose();
            //htw.Dispose();
            
            //Response.Write(sw.ToString());
            //Response.End();
            //return File(temp, "application/vnd.ms-excel");

            IEnumerable<HTAlarm> alarmlog;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}AlarmLog/filter", _uri);
                var result = await client.PostAsJsonAsync(uri, model);
                alarmlog = await result.Content.ReadAsAsync<IEnumerable<HTAlarm>>();
                TempData["AlarmLogList"] = alarmlog;
            }



            var reducedList = alarmlog.Select(e => new { e.ID, e.alarmname, e.alarmmessage, e.metername, e.starttimelog, e.stoptimelog }).ToList();
           
            var grid = new GridView();

            grid.DataSource = reducedList;
            grid.DataBind();
            grid.HeaderRow.Cells[0].Text = "ID";
            grid.HeaderRow.Cells[1].Text = "Alarm Name";
            grid.HeaderRow.Cells[2].Text = "Alarm Message";
            grid.HeaderRow.Cells[3].Text = "Meter Name";
            grid.HeaderRow.Cells[4].Text = "Start Time";
            grid.HeaderRow.Cells[5].Text = "End Time";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=AlarmLog_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));//
            grid.Dispose();
            htw.Dispose();
            return File(temp, "application/vnd.ms-excel");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateall(List<HTAlarm> sd)
        {

            try
            {

                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}AlarmLog/updateall", _uri);
                    var result = await client.PostAsJsonAsync(uri, sd);

                    return Json(new { Message = "Records updated successfully" }, JsonRequestBehavior.AllowGet);
                }

                //using (WebClient web = new WebClient())
                //{
                //    web.Headers.Add("Content-Type", "application/json");
                //    string status = web.UploadString(url + "AlarmLogAPI/updateallvalue", JsonConvert.SerializeObject(sd));
                //    //ViewBag.Msg = "Records updated successfully";

                //    return Json(new { Message = "Records updated successfully" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception)
            {
                return Json(new { Message = "Unable to proceed. Please try again later." }, JsonRequestBehavior.AllowGet);
                //throw;
            }


            //   return View("Index");





            //return View();

        }
    }
}