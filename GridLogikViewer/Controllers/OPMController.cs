using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class OPMController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /OPM/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Graph()
        {
            return View();
        }
        public string Process(string FromDate, string ToDate)
        {
            OPM obj = new OPM();
            obj.FromDate = FromDate;
            obj.ToDate = ToDate;


            string nextkey = WebConfigurationManager.AppSettings["Unit1"];
            int key = 1;

            List<string> portList = new List<string>();
            List<string> meterList = new List<string>();

            while (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Unit" + key]))
            {


                string port = WebConfigurationManager.AppSettings["Unit" + key].ToString();
                string meter = WebConfigurationManager.AppSettings["Unit" + key + "MeterId"].ToString();

                meterList.Add(meter);
                portList.Add(port);
                key++;
            }

            obj.MeterList = meterList;
            obj.PortList = portList;
            obj.TagList = WebConfigurationManager.AppSettings["Tags"].ToString().Split(',').ToList();

            string Result = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                Result = client.UploadString(url + "OPM", JsonConvert.SerializeObject(obj));
                // TempData["Success"] = "Record Added Successfully!";
            }
            return Result;
        }

        public void ExportToExcel(string FromDate, string ToDate)
        {
            DataTable exceldata = new DataTable(); 
            FromDate = FromDate.Replace('/', '-');
            ToDate = ToDate.Replace('/', '-');

            string FileName = "OPMData" + DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";   
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "OPM/GetExcelData/" + FromDate + "/" + ToDate);
                exceldata = JsonConvert.DeserializeObject<DataTable>(s);
            }
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
        }
    }
}