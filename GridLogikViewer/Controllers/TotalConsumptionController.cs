using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using GridLogik.ViewModels;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using GridLogikViewer.Models;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class TotalConsumptionController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string msg = string.Empty;  
        public ActionResult Index()
        {

            //Fill Meter group
            List<MeterGroup> MeterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
                MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }
            ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");

            //Fill Utility
            List<GridLogik.ViewModels.MstUtility> lstUtility = FillUtilities();
            ViewBag.UtilityID = new SelectList(lstUtility, "utilid", "utilname");
            return View();
        }
        private List<GridLogik.ViewModels.MstUtility> FillUtilities()
        {
            List<GridLogik.ViewModels.MstUtility> lstUtility = new List<GridLogik.ViewModels.MstUtility>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("utility").Result;
            if (response.IsSuccessStatusCode)
            {
                var utilities = response.Content.ReadAsStringAsync().Result;

                dynamic objUtilities = JValue.Parse(utilities);
                foreach (dynamic div in objUtilities.Data.result)
                {
                    lstUtility.Add(new GridLogik.ViewModels.MstUtility() { utilid = div.utilid, utilname = div.utilname });
                }



            }
            return lstUtility;
        }
        private List<MeterGroup> FillMeterGroup()
        {
            List<MeterGroup> lstGroup = new List<MeterGroup>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("metergroupapi?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"])).Result;
            if (response.IsSuccessStatusCode)
            {
                var groups = response.Content.ReadAsStringAsync().Result;

                dynamic objgroups = JValue.Parse(groups);
                foreach (dynamic grp in objgroups)
                {
                    if (grp.id != null && grp.groupname != null)

                        lstGroup.Add(new MeterGroup() { Id = grp.id, GroupName = grp.groupname });
                }
            }
            return lstGroup;
        }
        [HttpGet]

        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult MetersByGroupID(int id)
        {
            List<Meter> Meters = new List<Meter>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterAPI/GetMetersByGroupID/" + id);
                // string s = client.DownloadString(url + "MeterAPI/GetMetersByGroupID/" );
                Meters = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
            return Json(objMeters);
        }

        [HttpPost]
        public ActionResult MetersGroupsByUtilityID(int id)
        {
            List<MeterGroup> Meters = FillMeterGroup();

            SelectList objMeterGroup = new SelectList(Meters, "Id", "GroupName");
            return Json(objMeterGroup);
        }

        // C# Function to Convert Json string to C# Datatable
        protected DataTable ConvertJSONToDataTable(string jsonString, string pprd, string prd)
        {
            DataTable dt = new DataTable();
            //strip out bad characters
            string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            //hold column names
            List<string> dtColumns = new List<string>();

            //get columns
            foreach (string jp in jsonParts)
            {
                //only loop thru once to get column names
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1);
                        string v = rowData.Substring(idx + 1);
                        if (!dtColumns.Contains(n))
                        {
                            dtColumns.Add(n.Replace("\"", ""));
                        }
                    }
                    catch
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                    }

                }
                break; // TODO: might not be correct. Was : Exit For
            }

            //build dt
            foreach (string c in dtColumns)
            {
                dt.Columns.Add(c);
            }
            //get table data
            foreach (string jp in jsonParts)
            {
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string v = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[n] = v;
                    }
                    catch
                    {
                        continue;
                    }

                }
                dt.Rows.Add(nr);
            }


            //SetValue("PRRD", pprd, ref  dt);
            //SetValue("PRD", prd, ref  dt);
            RemoveColmnName(ref dt);
            ChangeColumnName(ref dt);
            RemoveBlankRow(ref  dt);
            return dt;
        }

        [HttpGet]
        public ActionResult ExportToExcel(string InputString)
        {

            List<DayWise> Meters = new List<DayWise>();
            string s = "";

            ExcelData objexceldata = new ExcelData();
            objexceldata.MeterId = InputString.Split('~')[0];
            objexceldata.FromDate = InputString.Split('~')[1];
           

            string FileName = "TotalConsumption_" + objexceldata.MeterId + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                s = client.UploadString(url + "TotalConsumptionApi", JsonConvert.SerializeObject(objexceldata));
            }

            DataTable exceldata = ConvertJSONToDataTable(s, objexceldata.FromDate, objexceldata.ToDate);
            if (exceldata == null || exceldata.Columns.Count == 0)
            {
                exceldata.Columns.Add("Reason");
                exceldata.AcceptChanges();
                DataRow dr1 = exceldata.NewRow();
                dr1["Reason"] = "No data Found for Excel Generation";
                exceldata.Rows.Add(dr1);
                exceldata.AcceptChanges();
            }


            var grid = new GridView();
            grid.DataSource = exceldata;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
        public void SetValue(string key, string value, ref DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[key] = value;
                    dt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

        }
        public void ChangeColumnName(ref DataTable dt)
        {
            try
            {
                string ColumnList = "REF~Ref,UFFNO~Office No,ALLOTTE_NAME~Allottee Name,PRRD~Previous Reading Date,PRD~Present Reading Date,MSN~Meter Sr.No.,METER_NUMBER~Meter Id,DESCRIPTION~Description,PF~PF,METER~Remark,ORKHW~Opening Reading KWh,CRKHW~Closing Reading KWh,CKHW~Consumption KWh,UR~Unit Rate(Rs),AMOUNT~Amount(Rs),ARREAR~Arrears,TA~Total Amount,TC~cummulative Reactive Energy ClosingReading,UC~cummulative Reactive Energy OpeningReading,TAR~Diff. cummulative Reactive Energy";
                string[] ColList = ColumnList.Split(',');
                if (dt != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < ColList.Length; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == ColList[i].Split('~')[0])
                            {
                                dc.ColumnName = Convert.ToString(ColList[i].Split('~')[1]);
                                dt.AcceptChanges();
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

        }
        public void RemoveColmnName(ref DataTable dt)
        {
            try
            {
                string ColumnList = "$id,ContentEncoding,ContentType,Data,result,PLF,JsonRequestBehavior,MaxJsonLength,RecursionLimit,TARR";
                string[] ColList = ColumnList.Split(',');
                if (dt != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < ColList.Length; i++)
                    {

                        if (dt.Columns.Contains(ColList[i]))
                            dt.Columns.Remove(ColList[i]);

                        dt.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

        }
        public void RemoveBlankRow(ref DataTable dt)
        {
            bool flag = false;
            string Meter_id = string.Empty;  
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Meter_id = Convert.ToString(dr.Field<string>("Meter Id"));
                    if (Meter_id == "null" || Meter_id == "")
                    {
                        dt.Rows.Remove(dr);
                        dt.AcceptChanges(); 
                        flag = true; 
                        break;
                    }
                }
                if (flag)
                {
                   RemoveBlankRow(ref dt);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
    public class ExcelData
    {
        public string MeterId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

}
