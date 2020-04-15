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
using System.Data;
using GridLogikViewer.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class GroupDisplayController : BaseController
    {

        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;

        //
        // GET: /GroupDisplay/
        public async Task<ActionResult> Index()
        {
            GroupDisplayViewModel groupdisplay = new GroupDisplayViewModel();


            groupdisplay.GroupNameList = await ListGroupName();
            //groupdisplay.QueryList = ListQuery();
            List<prmglobal> prmglobal = GetGlobalValues();
            ViewBag.QueryList = new SelectList(prmglobal, "prmvalue", "prmidentifier");
            groupdisplay.StartTimeList = TimeSlotList();
            groupdisplay.EndTimeList = TimeSlotList();

            groupdisplay.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            groupdisplay.ToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");

            //history.InstanceDataAverageLogList = new List<InstanceDataAverageLog>();
            return View("groupdisplay", groupdisplay);
        }

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

        private List<GroupTemplateQuery> ListQuery()
        {
            List<GroupTemplateQuery> querydetail = new List<GroupTemplateQuery>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "GroupTemplateQueryAPI");
                querydetail = JsonConvert.DeserializeObject<List<GroupTemplateQuery>>(s);
            }
            return querydetail;

        }

        private GroupTemplateQuery ListQueryParameter(string parameter)
        {
            GroupTemplateQuery querydetail = new GroupTemplateQuery();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "GroupDisplayQueryDataAPI/Get/" + parameter);
                querydetail = JsonConvert.DeserializeObject<GroupTemplateQuery>(s);
            }
            return querydetail;

        }

        private List<prmglobal> GetGlobalValues()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal/GetIdentifiersOnModuleNew/GroupDisplay").Result;
            List<prmglobal> lstGlobal = new List<prmglobal>();
            if (response.IsSuccessStatusCode)
            {
                var objResponse = response.Content.ReadAsStringAsync().Result;

                dynamic objPrmGlobal = JValue.Parse(objResponse);
                foreach (dynamic prm in objPrmGlobal)
                {
                    prmglobal obj = new prmglobal();
                    string prmUnit = (prm.prmunit.ToString().IndexOf('.') > 0 ? prm.prmunit.ToString().Substring(prm.prmunit.ToString().IndexOf('.'), (prm.prmunit.ToString().Length) - (prm.prmunit.ToString().IndexOf('.'))).ToString().Replace(".", "") : prm.prmunit.ToString());
                    if (prmUnit == "Parameter")
                    {
                        obj.prmidentifier = prm.prmidentifier.ToString();
                        obj.prmmodule = prm.prmmodule.ToString();
                        obj.prmrecid = Convert.ToInt16(prm.prmrecid.ToString());
                        obj.prmunit = prmUnit;
                        obj.prmvalue = prm.prmvalue.ToString();
                        obj.rfu1 = prm.rfu1.ToString();
                        obj.rfu2 = prm.rfu2.ToString();
                        lstGlobal.Add(obj);
                    }
                }
            }
            return lstGlobal.OrderBy(x => x.prmvalue).ToList();
        }


        private async Task<List<MeterGroup>> ListGroupName()
        {

            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI");
            //    meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}
            return meterGroups.ToList();
            //List<MeterGroup> groupname = new List<MeterGroup>();
            //using (WebClient client = new WebClient())
            //{
            //    string mg = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    groupname = JsonConvert.DeserializeObject<List<MeterGroup>>(mg);
            //}
            //return groupname;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupDisplayViewModel groupdisplay)
        {
            try
            {
                List<GroupDisplayMeterDataViewModel> meterlist = new List<GroupDisplayMeterDataViewModel>();
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "GroupDisplayAPI/GetGroupParameters", JsonConvert.SerializeObject(groupdisplay));
                    if (s != null)
                    {

                        ViewBag.MeterParameters = JsonConvert.DeserializeObject<List<string>>(s);
                        groupdisplay.ParametrsList = JsonConvert.DeserializeObject<List<string>>(s);
                    }
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");

                    string a = client.UploadString(url + "GroupDisplayAPI/GroupDisplayMeterData", JsonConvert.SerializeObject(groupdisplay));

                    DataTable obj = new DataTable();
                    if (a != null)
                    {
                        obj = JsonConvert.DeserializeObject<DataTable>(a);

                        foreach (DataColumn column in obj.Columns)
                            column.ColumnName = column.ColumnName.ToUpper();
                        obj.AcceptChanges();



                        ViewBag.groupdisplay = obj.AsEnumerable();

                    }
                }

                if (ViewBag.groupdisplay != null)
                {
                    groupdisplay.GroupNameList = await ListGroupName();
                    //groupdisplay.QueryList = ListQuery();
                    List<prmglobal> prmglobal = GetGlobalValues();
                    ViewBag.QueryList = new SelectList(prmglobal, "prmvalue", "prmidentifier");
                    return PartialView("_GroupDisplay", groupdisplay);
                }
                return PartialView("groupdisplay", groupdisplay);
            }
            catch
            {
                return View(groupdisplay);
            }


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(GroupDisplayViewModel groupdisplayquerydata, FormCollection form)
        {
            try
            {
                Boolean endDate = false;
                groupdisplayquerydata.StartTimeList = TimeSlotList();
                groupdisplayquerydata.EndTimeList = TimeSlotList();

                if (groupdisplayquerydata.StartTime == "select" || groupdisplayquerydata.StartTime == null)
                    groupdisplayquerydata.StartTime = "12:00 AM";


                if (groupdisplayquerydata.EndTime == "select" || groupdisplayquerydata.EndTime == null)
                {
                    endDate = true;
                }

                // groupdisplay.GroupNameList = ListGroupName();
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "GroupDisplayQueryDataAPI/GroupData", JsonConvert.SerializeObject(groupdisplayquerydata));
                    DataTable obj = new DataTable();
                    if (s != null)
                    {

                        obj = JsonConvert.DeserializeObject<DataTable>(s);
                        //TempData["InstanceDataAverageLogList"] = obj;
                        if (obj.Rows.Count == 0)
                            groupdisplayquerydata.flag = true;
                        else
                        {

                            foreach (DataColumn column in obj.Columns)
                                column.ColumnName = column.ColumnName.ToUpper();
                            obj.AcceptChanges();


                            ViewBag.groupdisplayquerydata = obj.AsEnumerable();
                            TempData["InstanceDataList"] = ViewBag.groupdisplayquerydata;
                            Session["InstanceDataList"] = obj;
                        }
                    }
                }

                if (ViewBag.groupdisplayquerydata != null)
                {
                    groupdisplayquerydata.GroupNameList = await ListGroupName();
                    groupdisplayquerydata.QueryList = ListQuery();
                    if (endDate || (groupdisplayquerydata.EndTime == "select" || groupdisplayquerydata.EndTime == null))
                    {
                        groupdisplayquerydata.EndTime = "12:00 AM";
                        DateTime fltrToDate = DateTime.ParseExact(groupdisplayquerydata.ToDate + " " + groupdisplayquerydata.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).AddDays(1);
                        groupdisplayquerydata.ToDate = fltrToDate.ToString("MM/dd/yyyy").Replace('-', '/');
                    }
                    return PartialView("GroupDisplayQuery", groupdisplayquerydata);
                }
                return PartialView("GroupDisplayQuery", groupdisplayquerydata);
            }
            catch
            {
                return View(groupdisplayquerydata);
            }


        }


        public ActionResult Export()
        {
            var grid = new GridView();
            grid.DataSource = Session["InstanceDataList"];
            grid.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Group_Display_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));
            grid.Dispose();
            htw.Dispose();
            return File(temp, "application/vnd.ms-excel");
        }


        //public void Export()
        //{
        //    var grid = new GridView();
        //    var data = TempData["InstanceDataList"];
        //    //foreach(var item in data)
        //    //{
        //    grid.DataSource = TempData["InstanceDataList"];
        //        grid.DataBind();
        //    //}

        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");

        //    Response.ContentType = "application/excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    grid.RenderControl(htw);

        //    Response.Write(sw.ToString());
        //    TempData["InstanceDataList"] = data;
        //    Response.End();
        //}

    }
}