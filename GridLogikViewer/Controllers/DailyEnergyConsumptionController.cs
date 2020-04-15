using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace GridLogikViewer.Controllers
{
    public class DailyEnergyConsumptionController : Controller
    {
        //
        // GET: /DailyEnergyConsumption/
        public ActionResult DailyEnergyConsumption()
        {
            DailyEnergyConsumptionAPI energyconsump = new DailyEnergyConsumptionAPI();
            List<MeterGroup> MeterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string url = WebConfigurationManager.AppSettings["APIUrl"];
                string s = client.DownloadString(url + "MeterGroupAPI");
                MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }
            //ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");
            energyconsump.MeterGroup = MeterGroup;
            return View("DailyEnergyConsumption", energyconsump);





           
        }



        [HttpGet]

        public ActionResult GetDailyEnergyConsmpExport(string fromday, string frommonth, string fromyear, string today, string tomonth, string toyear, string totime, string frmtime , int groupid)
        {
            string s = "";
            string url = WebConfigurationManager.AppSettings["APIUrl"];
            clsHistoryDataExportAPI objDR = new clsHistoryDataExportAPI();
            List<clsHistoryDataExportAPI> objDailyRpt = new List<clsHistoryDataExportAPI>();
            StringBuilder str = new StringBuilder();



            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                //s = client.DownloadString(url + "HistoryDataExportAPI/GetHistoryDataExport/" + fromday + "/" + frommonth + "/" + fromyear + "/" + today + "/" + tomonth + "/" +toyear+"/"+parameter);//, JsonConvert.SerializeObject(objDR));
                string fromdate = fromday + "-" + frommonth + "-" + fromyear + ":" + frmtime;
                string todate = today + "-" + tomonth + "-" + toyear + ":" + totime;

                s = client.DownloadString(url + "HistoryDataExportAPI/GetHistoryDataExport/" + fromdate + "/" + todate + "/" + "/" + groupid);
                objDailyRpt = JsonConvert.DeserializeObject<List<clsHistoryDataExportAPI>>(s);


            }

            return View("HistoryDataExport");
        }


	}
}