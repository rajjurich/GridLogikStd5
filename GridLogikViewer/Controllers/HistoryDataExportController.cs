using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using GridLogik.ViewModels;

namespace GridLogikViewer.Controllers
{
    public class HistoryDataExportController : Controller
    {
        //
        // GET: /HistoryDataExport/
        public ActionResult HistoryDataExport()
        {
            clsHistoryDataExportAPI history = new clsHistoryDataExportAPI();
            history.QueryList = ListQuery();
            List<MeterGroup> MeterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string url = WebConfigurationManager.AppSettings["APIUrl"];
                string s = client.DownloadString(url + "MeterGroupAPI");
                MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }
            //ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");
            history.MeterGroup = MeterGroup;
            return View("HistoryDataExport", history);
        }

        private List<prmglobal> ListQuery()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal/GetTablesIdentifiers/HistoryData").Result;
            List<prmglobal> lstGlobal = null;
            if (response.IsSuccessStatusCode)
            {
                var objResponse = response.Content.ReadAsStringAsync().Result;
                lstGlobal = new List<prmglobal>();
                dynamic objPrmGlobal = JValue.Parse(objResponse);
                foreach (dynamic prm in objPrmGlobal.Data.result)
                {
                    if (prm.prmmodule.ToString().ToLower() != "global")
                    {
                        prmglobal obj = new prmglobal();
                        obj.prmidentifier = prm.prmidentifier.ToString();
                        obj.prmmodule = prm.prmmodule.ToString();
                        obj.prmrecid = Convert.ToInt16(prm.prmrecid.ToString());
                        obj.prmunit = (prm.prmunit.ToString().IndexOf('.') > 0 ? prm.prmunit.ToString().Substring(prm.prmunit.ToString().IndexOf('.'), (prm.prmunit.ToString().Length) - (prm.prmunit.ToString().IndexOf('.'))).ToString().Replace(".", "") : prm.prmunit.ToString());
                        obj.prmvalue = prm.prmvalue.ToString();
                        obj.rfu1 = prm.rfu1.ToString();
                        obj.rfu2 = prm.rfu2.ToString();
                        lstGlobal.Add(obj);
                    }
                }
            }
            return lstGlobal;


        }

        [HttpGet]

        public ActionResult GetHistoryDataExport(string fromday, string frommonth, string fromyear, string today, string tomonth, string toyear, string parameter, int groupid)
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
                string fromdate = fromday + "-" + frommonth + "-" + fromyear;
                string todate = today + "-" + tomonth + "-" + toyear;

                s = client.DownloadString(url + "HistoryDataExportAPI/GetHistoryDataExport/" + fromdate + "/" + todate + "/" + parameter.Split('~')[0] + "/" + groupid);
                objDailyRpt = JsonConvert.DeserializeObject<List<clsHistoryDataExportAPI>>(s);


                fromdate = fromdate.Replace('-', '/');
                todate = todate.Replace('-', '/');
                //  DateTime dtTodaysDate = DateTime.Now;

                str.Append("<table border=1px>");

                str.Append("<tr>");
                str.Append("<td colspan=3>Report Period</td>");
                str.Append("</tr>");
                str.Append("<tr>");
                str.Append("<td></td>");
                str.Append("<td>Date</td>");
                str.Append("<td>Time</td>");
                str.Append("</tr>");

                str.Append("<tr>");
                str.Append("<td>From</td>");
                str.Append("<td>" + fromdate + "</td>");

                str.Append("</tr>");

                // dtTodaysDate = dtTodaysDate.AddDays(1);
                str.Append("<tr>");
                str.Append("<td>To</td>");
                str.Append("<td>" + todate + "</td>");

                str.Append("</tr>");

                //str.Append("<tr>");
                //str.Append("<td>Parameter</td>");
                //ViewBag.InstanceParameterID = new SelectList(ListQuery(), "prmrecid", "prmvalue");
                //str.Append("<td>" + ViewBag.InstanceParameterID + "</td>");


                //str.Append("</tr>");
                str.Append("<tr></tr>");
                str.Append("<tr>");
                str.Append("<td>Report Generation Parameter</td>");
                str.Append("<td>" + parameter.Split('~')[1] + "</td>");
                str.Append("</tr>");

                str.Append("<tr></tr>");
                str.Append("<tr>");
                str.Append("<td>Block No</td>");
                str.Append("<td>Date</td>");
                //get 96 blocks
                List<string> BlockNos = new List<string>();
                BlockNos = objDailyRpt.Select(x => x.blockno).Distinct().ToList();

                //List<DateTime?> BlockDate = new List<DateTime?>();
                //BlockDate = objDailyRpt.Select(x => x.tstamp).Distinct().ToList();

                List<long> MeterIds = new List<long>();
                MeterIds = objDailyRpt.Select(x => x.meterid).Distinct().ToList();

                for (int i = 0; i < BlockNos.Count; i++)
                {
                    str.Append("<td>" + BlockNos[i] + "</td>");
                }
                str.Append("</tr>");
                //str.Append("<tr>");
                //str.Append("<td>Timing</td>");
                //for (int i = 0; i < BlockDate.Count; i++)
                //{
                //    DateTime dtTime = Convert.ToDateTime(BlockDate[i]);
                //    DateTime dtBlockFrom = dtTime.Subtract(new TimeSpan(0, 15, 00));
                //    DateTime dtBlockTo = Convert.ToDateTime(dtTime);
                //    string day = dtTime.ToString("dd/MM/yyyy");

                //    string blktime = dtTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture) + " - " + dtTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                //    str.Append("<td>" + blktime + "</td>");
                //}
                //str.Append("</tr>");
                



                str.Append("<tr>");
                str.Append("<td>Line Meters</td>");
                str.Append("</tr>");

                for (int i = 0; i < MeterIds.Count; i++)
                {
                    DateTime Frmdate = CMSDateTime.CMSDateTime.ConvertToDateTime(fromdate.Replace('-', '/'), "dd/MM/yyyy");// Convert.ToDateTime(fromdate);
                    DateTime Todate = CMSDateTime.CMSDateTime.ConvertToDateTime(todate.Replace('-', '/'), "dd/MM/yyyy");

                    while (Frmdate <= Todate)
                    {
                        str.Append("<tr>");
                        long meterid = MeterIds[i];
                        // List<clsHistoryDataExportAPI> blkmetername = new List<clsHistoryDataExportAPI>();
                        var blkmetername = objDailyRpt.Where(x => x.meterid == meterid).Select(x => x.metername).Distinct().SingleOrDefault();
                        str.Append("<td>" + blkmetername + "</td>");
                        str.Append("<td>" + Frmdate.ToString("dd-MM-yyyy") + "</td>");

                        for (int j = 0; j < BlockNos.Count; j++)
                        {
                            string blkno = BlockNos[j];

                            List<clsHistoryDataExportAPI> BlkMeterData = new List<clsHistoryDataExportAPI>();
                            BlkMeterData = objDailyRpt.Select(x => x).Where(x => x.meterid == meterid && x.blockno == blkno && x.date == Frmdate.ToString("ddMMyyyy")).ToList();

                            if (BlkMeterData.Count == 0)
                            {
                                str.Append("<td>0</td>");
                            }

                            for (int k = 0; k < BlkMeterData.Count; k++)
                            {

                                str.Append("<td>" + BlkMeterData[k].parameter + "</td>");

                            }

                        }
                        str.Append("</tr>");
                        Frmdate = Convert.ToDateTime(Frmdate).AddDays(1);
                    }



                }

                //for (int i = 0; i < objDailyRpt.Count; i++)
                //{
                //    for (int j = 0; j < BlockNos.Count; j++)
                //    {
                //        string blkno = BlockNos[j];

                //        //BlkMeterData=objDailyRpt.Select(x =>x.)

                //        for (int k = 0; k < MeterIds.Count; k++)
                //        {
                //            List<clsDailyReport> BlkMeterData = new List<clsDailyReport>();
                //            BlkMeterData = objDailyRpt.Select(x => x).Where(x => x.meterid == MeterIds[k] && x.blockno == blkno).ToList();
                //            str.Append("<td><font face=Arial Narrow size=3>" + MeterIds[k] + "</td>");

                //            if (BlkMeterData.Count==0)
                //            {
                //                str.Append("<td></td>");
                //            }

                //            for (int l = 0; l < BlkMeterData.Count; l++)
                //            {
                //                if (blkno == objDailyRpt[i].blockno && objDailyRpt[i].meterid == MeterIds[k])
                //                {
                //                    //str.Append("<td><font face=Arial Narrow size=3>" + objDailyRpt[i].meterid + "</td>");
                //                    if (parameter == "")
                //                    {
                //                        str.Append("<td><font face=Arial Narrow size=3>" + objDailyRpt[i].kwh_imp + "</td>");
                //                    }
                //                    else
                //                    {
                //                        str.Append("<td><font face=Arial Narrow size=3>" + objDailyRpt[i].kwh_exp + "</td>");
                //                    }
                //                }
                //                else
                //                {
                //                    str.Append("<td></td>");
                //                }
                //            }

                //        }

                //    }
                //}




                str.Append("</tr>");

                str.Append("</table>");


                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=HistoryData_Report_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
                this.Response.ContentType = "application/vnd.ms-excel";
                byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
                return File(temp, "application/vnd.ms-excel");


                //ends here



                //str.Append("<td><font face=Arial Narrow size=3>LName</td>");
                //str.Append("<td><font face=Arial Narrow size=3>Address</td>");
                //str.Append("</tr>");

            }

           // return View("HistoryDataExport");
        }
    }
}