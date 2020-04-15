using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridLogikViewer.Controllers
{
    public class MeterViewController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        public async Task<ActionResult> Index()
        {
            //InstanceData model = new InstanceData();
            ////model.Meters = ListMeterModel();
            //model.Groups = ListMeterGroup();
            //model. 
            //model.CurrentDate = DateTime.Now;
            //return View("Index", model);


            ProfileLogViewModel model = new ProfileLogViewModel();
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            model.Groups = await ListMeterGroup();
            //model.FromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            //model.ToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            // model.LoadServiceList = new List<LoadService>();
            // ViewBag.RecordCount = "false";
            return View("Index", model);
            //return View("Index");
        }

        public async Task<ActionResult> IndexHist()
        {
            ProfileLogViewModel model = new ProfileLogViewModel();
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            model.Groups = await ListMeterGroup();

            return View("IndexHist", model);
        }

        private async Task<List<MeterGroup>> ListMeterGroup()
        {
            //List<MeterGroup> meterGroup = new List<MeterGroup>();
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}
            return meterGroups.ToList();
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
        public ActionResult Export(String InputString, String InputStr)
        {
            DataTable obj = new DataTable();
            string Lbl = string.Empty;
            string values = string.Empty;
            try
            {
                string[] InputData = InputString.Split('~');
                string[] labelData = InputStr.Split('~');
                Lbl = "Group Name ~Meter Name ~Query ~From Date ~Time ~To Date ~Time ~Interval";
                CommonViewModel objData = new CommonViewModel();
                var meters = labelData[0].Split(',').Select(x => Convert.ToInt64(x)).ToList();
                objData.Meters = meters;
                
                objData.GroupId = 1;
                objData.StartTime = Convert.ToString(labelData[1]);
                objData.EndTime = Convert.ToString(labelData[2]);
                objData.FromDate = Convert.ToString(labelData[3]);
                objData.ToDate = Convert.ToString(labelData[4]);
                var parameters = labelData[6].Split(',').ToList();                
                objData.Params = parameters;
                objData.Interval = Convert.ToString(labelData[5]);

                values = Convert.ToString(InputData[0]) + "~" + Convert.ToString(InputData[1]) + "~" + Convert.ToString(InputData[3]) + "~" + Convert.ToString(labelData[3]) + "~" + Convert.ToString(labelData[1]) + "~" + Convert.ToString(labelData[4]) + "~" + Convert.ToString(labelData[2]) + "~" + Convert.ToString(InputData[2]);
                using (WebClient client = new WebClient())
                {
                    var s = new JsonResult();
                    client.Headers.Add("Content-Type", "application/json");
                    string s1 = client.UploadString(url + "MeterView/CommonView", JsonConvert.SerializeObject(objData));
                    if (s1 != null || s1 != "null")
                    {
                        List<LoadService> LoadServiceList = JsonConvert.DeserializeObject<List<LoadService>>(s1);

                        obj = JsonConvert.DeserializeObject<DataTable>(s1);
                        foreach (DataColumn column in obj.Columns)
                            column.ColumnName = column.ColumnName.ToUpper();
                        obj.AcceptChanges();

                        foreach (var column in obj.Columns.Cast<DataColumn>().ToArray())
                        {
                            if (obj.AsEnumerable().All(dr => dr.IsNull(column)))
                                obj.Columns.Remove(column);
                        }
                        obj.AcceptChanges();
                        if (obj.Columns.Contains("$ID") || obj.Columns.Contains("$id"))
                        {
                            obj.Columns.Remove("$ID");
                            obj.AcceptChanges();
                        }
                        if (obj.Columns.Contains("ID") || obj.Columns.Contains("id"))
                        {
                            obj.Columns.Remove("ID");
                            obj.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                obj = null;
            }
            if (obj == null || obj.Columns.Count == 0)
            {
                obj.Columns.Add("Reason");
                obj.AcceptChanges();
                DataRow dr1 = obj.NewRow();
                dr1["Reason"] = "No data Found for Excel Generation";
                obj.Rows.Add(dr1);
                obj.AcceptChanges();
            }
            StringBuilder str = getString(Convert.ToString("Meter View"), Lbl.Split('~'), values.Split('~'));
            var grid = new GridView();
            grid.DataSource = obj;
            grid.DataBind();
            StringWriter sw = new StringWriter(str);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Query_Data_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));
            grid.Dispose();
            htw.Dispose();
            return File(temp, "application/vnd.ms-excel");
        }
        public static StringBuilder getString(string headerText, string[] paramlabel, string[] paramvalue)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<b>" + headerText + "</b><br><br>");
            if (paramlabel != null && paramlabel.Length > 0)
            {
                for (int i = 0; i < paramlabel.Length; i++)
                {
                    str.Append("<b>" + paramlabel[i] + " :</b> " + paramvalue[i] + "   ");
                }
            }
            str.Append("<br><br>");
            return str;
        }
    }
}