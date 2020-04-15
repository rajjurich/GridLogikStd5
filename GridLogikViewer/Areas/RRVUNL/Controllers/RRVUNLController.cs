using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EToolsViewer.Areas.RRVUNL.Controllers
{
    public class RRVUNLController : Controller
    {

        public ActionResult AHP()
        {
            return View("AHP");
        }
        public ActionResult BOILERAUXILIARIES()
        {
            return View("BOILERAUXILIARIES");
        }
        public ActionResult CHPArea()
        {
            return View("CHPArea");
        }
        public ActionResult EMSMeterDetails()
        {
            return View("EMSMeterDetails");
        }
        public ActionResult FEEDERSArea()
        {
            return View("FEEDERSArea");
        }
        public ActionResult OTHERAUX()
        {
            return View("OTHERAUX");
        }
        public ActionResult TURBINEAUX()
        {
            return View("TURBINEAUX");
        }
        public ActionResult Screen1()
        {
            ViewBag.FromDate = DateTime.Today;
            ViewBag.StartTimeList = TimeSlotList();// getBlockArray();

            return View("Screen1");
        }
        public ActionResult Screen2()
        {
            return View("Screen2");
        }
        public ActionResult Screen3()
        {
            return View("Screen3");
        }
        public ActionResult Screen4()
        {
            return View("Screen4");
        }
        public ActionResult Screen5()
        {
            return View("Screen5");
        }
        public ActionResult Screen6()
        {
            return View("Screen6");
        }
        public ActionResult Screen7()
        {
            return View("Screen7");
        }
        public ActionResult Screen8()
        {
            return View("Screen8");
        }
        public ActionResult Screen9()
        {
            return View("Screen9");
        }
        public ActionResult Screen10()
        {
            return View("Screen10");
        }
        public ActionResult Screen11()
        {
            return View("Screen11");
        }
        public ActionResult Screen12()
        {
            return View("Screen12");
        }
        public ActionResult Screen13()
        {
            return View("Screen13");
        }
        public ActionResult Screen14()
        {
            return View("Screen14");
        }
        public ActionResult Screen15()
        {
            return View("Screen15");
        }
        public ActionResult Screen16()
        {
            return View("Screen16");
        }
        public ActionResult Screen17()
        {
            return View("Screen17");
        }
        public ActionResult Screen18()
        {
            return View("Screen18");
        }
        public ActionResult Screen19()
        {
            return View("Screen19");
        }
        public ActionResult Screen20()
        {
            return View("Screen20");
        }
        public ActionResult Screen21()
        {
            return View("Screen21");
        }
        public ActionResult Screen22()
        {
            return View("Screen22");
        }
        public ActionResult Screen23()
        {
            return View("Screen23");
        }
        public ActionResult Screen24()
        {
            return View("Screen24");
        }
        public ActionResult Screen25()
        {
            return View("Screen25");
        }
        public ActionResult Screen26()
        {
            return View("Screen26");
        }
        public ActionResult Screen27()
        {
            return View("Screen27");
        }
        public ActionResult Screen28()
        {
            return View("Screen28");
        }
        public ActionResult Screen29()
        {
            return View("Screen29");
        }
        public ActionResult Screen30()
        {
            return View("Screen30");
        }
        public ActionResult Screen31()
        {
            return View("Screen31");
        }
        public ActionResult Screen32()
        {
            return View("Screen32");
        }
        public ActionResult Screen33()
        {
            return View("Screen33");
        }
        public ActionResult Screen34()
        {
            return View("Screen34");
        }
        public ActionResult Screen35()
        {
            return View("Screen35");
        }
        public ActionResult Screen36()
        {
            return View("Screen36");
        }
        public ActionResult Screen37()
        {
            return View("Screen37");
        }
        public ActionResult Screen38()
        {
            return View("Screen38");
        }
        public ActionResult Screen39()
        {
            return View("Screen39");
        }
        public ActionResult Screen40()
        {
            return View("Screen40");
        }
        public ActionResult Screen41()
        {
            return View("Screen41");
        }
        public ActionResult Screen42()
        {
            return View("Screen42");
        }
        public ActionResult Screen43()
        {
            return View("Screen43");
        }
        public ActionResult Screen44()
        {

            ViewBag.FromDate = DateTime.Today;
            ViewBag.StartTimeList = TimeSlotList();// getBlockArray();
            ViewBag.StartTime = DateTime.Now;
            return View("Screen44");
        }
        public ActionResult Screen45()
        {
            ViewBag.FromDate = DateTime.Today;
            ViewBag.StartTimeList = TimeSlotList();// getBlockArray();
            //ViewBag.StartTime = DateTime.Now;
            return View("Screen45");
        }

        private Dictionary<int, string> TimeSlotList()
        {
            var list = new List<string>();
            DateTime start = new DateTime(0001, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(0001, 1, 1, 23, 59, 0);
            DateTime current = start;
            Dictionary<int, DateTime> dictionary =
new Dictionary<int, DateTime>();

            int i = 1;
            while (current <= end)
            {
                DateTime dt = default(DateTime).Add(DateTime.Now.TimeOfDay);
                list.Add(current.ToString("HH:mm"));//
                dictionary.Add(i, current);
                current = current.AddMinutes(15);
                i++;
            }

            int k = dictionary.Last(x => x.Value < default(DateTime).Add(DateTime.Now.TimeOfDay)).Key;
            if (k >= 1 && k != 96)
            {
                Dictionary<int, string> filteredlist = new Dictionary<int, string>();
                filteredlist = dictionary.Where(x => x.Key > k).ToDictionary(p => p.Key, p => p.Value.ToString("HH:mm"));
                ViewBag.StartTime = DateTime.Now;
                return filteredlist;
            }
            else
            {
                if (k == 96)
                {
                    ViewBag.StartTime = DateTime.Now.AddDays(1);
                    return dictionary.ToDictionary(p => p.Key, p => p.Value.ToString("HH:mm")); ;
                }
                else
                {
                    ViewBag.StartTime = DateTime.Now;
                    return dictionary.ToDictionary(p => p.Key, p => p.Value.ToString("HH:mm")); ;
                }
            }
            //   return dictionary;
        }



        public int getBlockNumber(string HourMinute)
        {
            try
            {
                string[] bArray = getBlockArray();
                if (bArray != null && bArray.Length > 0)
                {
                    for (int i = 1; i < bArray.Length + 1; i++)
                    {
                        if (Convert.ToString(bArray[i]).Contains('-'))
                        {
                            if (Convert.ToString(bArray[i]).Split('-')[1] == HourMinute)
                            {
                                return Convert.ToInt32(Convert.ToString(bArray[i]).Split('-')[0]);
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                //Logger.Log("getBlockNumber " + ex);
                return 0;
            }
        }
        public string[] getBlockArray()
        {
            string[] bArray = new string[97];
            try
            {
                bArray[1] = "1-00:15";
                bArray[2] = "2-00:30";
                bArray[3] = "3-00:45";
                bArray[4] = "4-01:00";
                bArray[5] = "5-01:15";
                bArray[6] = "6-01:30";
                bArray[7] = "7-01:45";
                bArray[8] = "8-02:00";
                bArray[9] = "9-02:15";
                bArray[10] = "10-02:30";
                bArray[11] = "11-02:45";
                bArray[12] = "12-03:00";
                bArray[13] = "13-03:15";
                bArray[14] = "14-03:30";
                bArray[15] = "15-03:45";
                bArray[16] = "16-04:00";
                bArray[17] = "17-04:15";
                bArray[18] = "18-04:30";
                bArray[19] = "19-04:45";
                bArray[20] = "20-05:00";
                bArray[21] = "21-05:15";
                bArray[22] = "22-05:30";
                bArray[23] = "23-05:45";
                bArray[24] = "24-06:00";
                bArray[25] = "25-06:15";
                bArray[26] = "26-06:30";
                bArray[27] = "27-06:45";
                bArray[28] = "28-07:00";
                bArray[29] = "29-07:15";
                bArray[30] = "30-07:30";
                bArray[31] = "31-07:45";
                bArray[32] = "32-08:00";
                bArray[33] = "33-08:15";
                bArray[34] = "34-08:30";
                bArray[35] = "35-08:45";
                bArray[36] = "36-09:00";
                bArray[37] = "37-09:15";
                bArray[38] = "38-09:30";
                bArray[39] = "39-09:45";
                bArray[40] = "40-10:00";
                bArray[41] = "41-10:15";
                bArray[42] = "42-10:30";
                bArray[43] = "43-10:45";
                bArray[44] = "44-11:00";
                bArray[45] = "45-11:15";
                bArray[46] = "46-11:30";
                bArray[47] = "47-11:45";
                bArray[48] = "48-12:00";
                bArray[49] = "49-12:15";
                bArray[50] = "50-12:30";
                bArray[51] = "51-12:45";
                bArray[52] = "52-13:00";
                bArray[53] = "53-13:15";
                bArray[54] = "54-13:30";
                bArray[55] = "55-13:45";
                bArray[56] = "56-14:00";
                bArray[57] = "57-14:15";
                bArray[58] = "58-14:30";
                bArray[59] = "59-14:45";
                bArray[60] = "60-15:00";
                bArray[61] = "61-15:15";
                bArray[62] = "62-15:30";
                bArray[63] = "63-15:45";
                bArray[64] = "64-16:00";
                bArray[65] = "65-16:15";
                bArray[66] = "66-16:30";
                bArray[67] = "67-16:45";
                bArray[68] = "68-17:00";
                bArray[69] = "69-17:15";
                bArray[70] = "70-17:30";
                bArray[71] = "71-17:45";
                bArray[72] = "72-18:00";
                bArray[73] = "73-18:15";
                bArray[74] = "74-18:30";
                bArray[75] = "75-18:45";
                bArray[76] = "76-19:00";
                bArray[77] = "77-19:15";
                bArray[78] = "78-19:30";
                bArray[79] = "79-19:45";
                bArray[80] = "80-20:00";
                bArray[81] = "81-20:15";
                bArray[82] = "82-20:30";
                bArray[83] = "83-20:45";
                bArray[84] = "84-21:00";
                bArray[85] = "85-21:15";
                bArray[86] = "86-21:30";
                bArray[87] = "87-21:45";
                bArray[88] = "88-22:00";
                bArray[89] = "89-22:15";
                bArray[90] = "90-22:30";
                bArray[91] = "91-22:45";
                bArray[92] = "92-23:00";
                bArray[93] = "93-23:15";
                bArray[94] = "94-23:30";
                bArray[95] = "95-23:45";
                bArray[96] = "96-00:00";
            }
            catch (Exception ex)
            {
                //Logger.Log("getBlockArray " + ex);
            }
            return bArray;
        }
        public ActionResult Screen46()
        {
            return View("Screen46");
        }
        public ActionResult Screen47()
        {
            return View("Screen47");
        }
        public ActionResult Screen48()
        {
            return View("Screen48");
        }
        public ActionResult Screen49()
        {
            return View("Screen49");
        }
        public ActionResult Screen50()
        {
            return View("Screen50");
        }

        public ActionResult Screen51()
        {
            return View("Screen51");
        }
        public ActionResult Screen52()
        {
            return View("Screen52");
        }
        public ActionResult Screen53()
        {
            return View("Screen53");
        }
        public ActionResult Screen54()
        {
            return View("Screen54");
        }
        public ActionResult Screen55()
        {
            return View("Screen55");
        }
    }
}