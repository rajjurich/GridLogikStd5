using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class QueryViewController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /QueryView/
        public async Task<ActionResult> Index()
        {

            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.AddTokenToHeader("");
                uri = string.Format("{0}prmglobal/GetIdentifiersOnModuleNew/QueryView", _uri);
                var result = await client.GetAsync(uri);
                List<prmglobal> content = await result.Content.ReadAsAsync<List<prmglobal>>();
                var tables = content.Select(d => new SelectListItem
                {
                    Value = d.prmvalue,
                    Text = d.prmvalue
                });
                ViewBag.Tables = tables;
              
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
                var Schedule = new List<SelectListItem>();
                foreach (var item in list)
                {
                    Schedule.Add(new SelectListItem() { Text = item, Value = item });
                }



                ViewBag.Schedule = Schedule;

                var Interval = new List<SelectListItem>();
                Interval.Add(new SelectListItem() { Text = "Hour Wise", Value = "H" });
                Interval.Add(new SelectListItem() { Text = "Block Wise", Value = "B" });
                Interval.Add(new SelectListItem() { Text = "Half Hour Wise", Value = "A" });
                Interval.Add(new SelectListItem() { Text = "Day Wise", Value = "D" });

                ViewBag.Interval = Interval;

                ViewBag.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                ViewBag.ToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");



            }
        }

        public class TblData
        {
            public string tableName { get; set; }
            public string columnName { get; set; }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(QueryViewModel collection)
        {
            DataTable obj = new DataTable();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}QueryView", _uri);

                        var result = await client.PostAsJsonAsync(uri, collection);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            obj = JsonConvert.DeserializeObject<DataTable>(contents);
                            TempData["HistoryDataList"] = obj;
                            Session["HistoryDataList"] = obj;
                            ViewBag.HistoryData = obj.AsEnumerable();
                            if (ViewBag.HistoryData != null)
                            {

                                return View("QueryView", collection);
                            }
                            return View("QueryView", collection);
                            //return RedirectToAction("Index");
                        }
                        else
                        {
                            await BindDropDown();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    await BindDropDown();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /QueryView/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /QueryView/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /QueryView/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /QueryView/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /QueryView/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /QueryView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /QueryView/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void Export(HistoryViewModel model)
        {
            List<HistoryViewModel> historydatalist = new List<HistoryViewModel>();

            var grid = new GridView();
            var data = TempData["HistoryDataList"];
            grid.DataSource = TempData["HistoryDataList"];
            grid.DataBind();


            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=QueryViewData_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xls");

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
