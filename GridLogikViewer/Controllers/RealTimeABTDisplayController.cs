using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class RealTimeABTDisplayController : Controller
    {
        //

         public ActionResult DailyReport()
         {
             return View();
         }


        // GET: /RealTimeABTDisplay/
        
         [HttpGet]
         public ActionResult GetDailyReport(string day, string month, string year,string parameter)
         {
             string s = "";
             string url = WebConfigurationManager.AppSettings["APIUrl"];
             clsDailyReport objDR = new clsDailyReport();
             List<clsDailyReport> objDailyRpt = new List<clsDailyReport>();
             StringBuilder str = new StringBuilder();
             using (WebClient client = new WebClient())
             {
                 client.Headers.Add("Content-Type", "application/json");
                 s = client.DownloadString(url + "RealTimeABTDisplayAPI/GetDailyReport/" + day + "/" + month + "/" + year);//, JsonConvert.SerializeObject(objDR));
                 objDailyRpt = JsonConvert.DeserializeObject<List<clsDailyReport>>(s);

                 DateTime dtTodaysDate = DateTime.Now;
                 
                 
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
                 str.Append("<td>" + dtTodaysDate.Day.ToString() + "/" + dtTodaysDate.Month.ToString() + "/" + dtTodaysDate.Year.ToString() + "</td>");
                 str.Append("<td>00:00:00</td>");
                 str.Append("</tr>");

                 dtTodaysDate = dtTodaysDate.AddDays(1);
                 str.Append("<tr>");
                 str.Append("<td>To</td>");
                 str.Append("<td>" + dtTodaysDate.Day.ToString() + "/" + dtTodaysDate.Month.ToString() + "/" + dtTodaysDate.Year.ToString() + "</td>");
                 str.Append("<td>00:00:00</td>");
                 str.Append("</tr>");
                 str.Append("<tr></tr>");
                 str.Append("<tr>");
                 str.Append("<td>Report Generation Time</td>");
                 str.Append("<td>" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "</td>");
                 str.Append("</tr>");

                 str.Append("<tr></tr>");
                 str.Append("<tr>");
                 str.Append("<td>Block No</td>");

                 //get 96 blocks
                 List<string> BlockNos = new List<string>();
                 BlockNos = objDailyRpt.Select(x => x.blockno).Distinct().ToList();

                 List<DateTime?> BlockDate = new List<DateTime?>();
                 BlockDate = objDailyRpt.Select(x => x.tstamp).Distinct().ToList();

                 List<long> MeterIds = new List<long>();
                 MeterIds = objDailyRpt.Select(x => x.meterid).Distinct().ToList();

                 for (int i = 0; i < BlockNos.Count; i++)
                 {
                     str.Append("<td>" + BlockNos[i] + "</td>");
                 }
                 str.Append("</tr>");
                 str.Append("<tr>");
                 str.Append("<td>Timing</td>");                
                 for (int i = 0; i < BlockDate.Count; i++)
                 {
                     DateTime dtTime = Convert.ToDateTime(BlockDate[i]);
                     DateTime dtBlockFrom = dtTime.Subtract(new TimeSpan(0, 15, 00));
                     DateTime dtBlockTo = Convert.ToDateTime(dtTime);


                     string blktime = dtTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture) + " - " + dtTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                     str.Append("<td>" + blktime + "</td>");
                 }
                 str.Append("</tr>");


                 str.Append("<tr>");
                 str.Append("<td>Line Meters</td>");
                 str.Append("</tr>");

                 for (int i  = 0; i < MeterIds.Count; i++)
                 {
                     str.Append("<tr>");
                     long meterid = MeterIds[i];
                     str.Append("<td>" + meterid + "</td>");
                     for (int j = 0; j < BlockNos.Count; j++)
                     {
                         string blkno = BlockNos[j];

                         List<clsDailyReport> BlkMeterData = new List<clsDailyReport>();
                         BlkMeterData = objDailyRpt.Select(x => x).Where(x => x.meterid == meterid && x.blockno == blkno).ToList();
                         
                         if (BlkMeterData.Count == 0)
                         {
                             str.Append("<td>0</td>");
                         }

                         for (int k = 0; k < BlkMeterData.Count; k++)
                         {
                             if (parameter == "kwhimp")
                             {
                                 str.Append("<td>" + BlkMeterData[k].kwh_imp + "</td>");
                             }
                             else if (parameter == "kwhexp")
                             {
                                 str.Append("<td>" + BlkMeterData[k].kwh_exp + "</td>");
                             }
                         }

                     }

                     str.Append("</tr>");


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


                 HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Daily_Report_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
                 this.Response.ContentType = "application/vnd.ms-excel";
                 byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
                 return File(temp, "application/vnd.ms-excel");


                 //ends here



                 //str.Append("<td><font face=Arial Narrow size=3>LName</td>");
                 //str.Append("<td><font face=Arial Narrow size=3>Address</td>");
                 //str.Append("</tr>");

             }

            // return View();
         }
    }
}