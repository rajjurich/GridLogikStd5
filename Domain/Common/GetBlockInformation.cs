using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class GetBlockInformation
    {
        public static int GetBlockNumber()
        {
            DateTime dtBlockNoDate = DateTime.Now;
            DateTime dtDateBlockFrom = new DateTime(dtBlockNoDate.Year, dtBlockNoDate.Month, dtBlockNoDate.Day);
            TimeSpan span = dtBlockNoDate - dtDateBlockFrom;
            double minutes = span.TotalMinutes;

            int iBlockNo = Convert.ToInt16(minutes) / 15;
            return iBlockNo;
        }

        private static Hashtable getBlockList(ref Hashtable _ht)
        {
            try
            {
                _ht.Add(1, "00:15");
                _ht.Add(2, "00:30");
                _ht.Add(3, "00:45");
                _ht.Add(4, "01:00");
                _ht.Add(5, "01:15");
                _ht.Add(6, "01:30");
                _ht.Add(7, "01:45");
                _ht.Add(8, "02:00");
                _ht.Add(9, "02:15");
                _ht.Add(10, "02:30");
                _ht.Add(11, "02:45");
                _ht.Add(12, "03:00");
                _ht.Add(13, "03:15");
                _ht.Add(14, "03:30");
                _ht.Add(15, "03:45");
                _ht.Add(16, "04:00");
                _ht.Add(17, "04:15");
                _ht.Add(18, "04:30");
                _ht.Add(19, "04:45");
                _ht.Add(20, "05:00");
                _ht.Add(21, "05:15");
                _ht.Add(22, "05:30");
                _ht.Add(23, "05:45");
                _ht.Add(24, "06:00");
                _ht.Add(25, "06:15");
                _ht.Add(26, "06:30");
                _ht.Add(27, "06:45");
                _ht.Add(28, "07:00");
                _ht.Add(29, "07:15");
                _ht.Add(30, "07:30");
                _ht.Add(31, "07:45");
                _ht.Add(32, "08:00");
                _ht.Add(33, "08:15");
                _ht.Add(34, "08:30");
                _ht.Add(35, "08:45");
                _ht.Add(36, "09:00");
                _ht.Add(37, "09:15");
                _ht.Add(38, "09:30");
                _ht.Add(39, "09:45");
                _ht.Add(40, "10:00");
                _ht.Add(41, "10:15");
                _ht.Add(42, "10:30");
                _ht.Add(43, "10:45");
                _ht.Add(44, "11:00");
                _ht.Add(45, "11:15");
                _ht.Add(46, "11:30");
                _ht.Add(47, "11:45");
                _ht.Add(48, "12:00");
                _ht.Add(49, "12:15");
                _ht.Add(50, "12:30");
                _ht.Add(51, "12:45");
                _ht.Add(52, "13:00");
                _ht.Add(53, "13:15");
                _ht.Add(54, "13:30");
                _ht.Add(55, "13:45");
                _ht.Add(56, "14:00");
                _ht.Add(57, "14:15");
                _ht.Add(58, "14:30");
                _ht.Add(59, "14:45");
                _ht.Add(60, "15:00");
                _ht.Add(61, "15:15");
                _ht.Add(62, "15:30");
                _ht.Add(63, "15:45");
                _ht.Add(64, "16:00");
                _ht.Add(65, "16:15");
                _ht.Add(66, "16:30");
                _ht.Add(67, "16:45");
                _ht.Add(68, "17:00");
                _ht.Add(69, "17:15");
                _ht.Add(70, "17:30");
                _ht.Add(71, "17:45");
                _ht.Add(72, "18:00");
                _ht.Add(73, "18:15");
                _ht.Add(74, "18:30");
                _ht.Add(75, "18:45");
                _ht.Add(76, "19:00");
                _ht.Add(77, "19:15");
                _ht.Add(78, "19:30");
                _ht.Add(79, "19:45");
                _ht.Add(80, "20:00");
                _ht.Add(81, "20:15");
                _ht.Add(82, "20:30");
                _ht.Add(83, "20:45");
                _ht.Add(84, "21:00");
                _ht.Add(85, "21:15");
                _ht.Add(86, "21:30");
                _ht.Add(87, "21:45");
                _ht.Add(88, "22:00");
                _ht.Add(89, "22:15");
                _ht.Add(90, "22:30");
                _ht.Add(91, "22:45");
                _ht.Add(92, "23:00");
                _ht.Add(93, "23:15");
                _ht.Add(94, "23:30");
                _ht.Add(95, "23:45");
                _ht.Add(96, "00:00");

                return _ht;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static string getBlockTime(int blocknumber)
        {
            Hashtable ht = new Hashtable();
            ht = getBlockList(ref ht);
            int a = ht.Count;
            return ht[blocknumber].ToString();
        }

        public static long getTimeStampId(DateTime? dt)
        {
            TimeSpan t = Convert.ToDateTime(dt) - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)t.TotalSeconds;

            return secondsSinceEpoch;
        }

        public static string GetDateCondition(string condition, string Time)
        {

            int blockno = GetBlockNumber();
            string blocktime = getBlockTime(blockno);
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            DateTime firstOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dateOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime lastOfPreviousMonth = firstOfCurrentMonth.AddDays(-1);
            DateTime firstOfPreviousMonth = new DateTime(lastOfPreviousMonth.Year, lastOfPreviousMonth.Month, 1);

            DateTime firstOfCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);

            DateTime firstOfCurrentFinancialYear = new DateTime(DateTime.Now.Year, 4, 1);

            DateTime firstOfPreviousFinancialYear = new DateTime((DateTime.Now.Year - 1), 4, 1);

            DateTime firstOfPreviousYear = new DateTime((DateTime.Now.Year - 1), 1, 1);

            DateTime lastOfPreviousFinancialYear = firstOfCurrentFinancialYear.AddDays(-1);
            DateTime lastOfPreviousYear = firstOfCurrentYear.AddDays(-1);
            switch (condition)
            {
                case "Current Block":
                    blockno = GetBlockNumber();
                    blocktime = getBlockTime(blockno);
                    dt = DateTime.Now.ToString("yyyy-MM-dd") + " " + blocktime + ":00";
                    return dt;
                case "Previous Block":
                    blockno = GetBlockNumber() - 1;
                    blocktime = getBlockTime(blockno);
                    dt = DateTime.Now.ToString("yyyy-MM-dd") + " " + blocktime + ":00";
                    return dt;
                case "Current Date":
                    dt = DateTime.Now.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Date":
                    dt = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Month From":
                    dt = firstOfCurrentMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Month To":
                    dt = dateOfCurrentMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Month From":
                    dt = firstOfPreviousMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Month To":
                    dt = lastOfPreviousMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Year From":
                    dt = firstOfCurrentYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Year To":
                    dt = dateOfCurrentMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Year From":
                    dt = firstOfPreviousYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Year To":
                    dt = lastOfPreviousYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Financial Year From":
                    dt = firstOfCurrentFinancialYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Current Financial Year To":
                    dt = dateOfCurrentMonth.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Financial Year From":
                    dt = firstOfPreviousFinancialYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                case "Previous Financial Year To":
                    dt = lastOfPreviousFinancialYear.ToString("yyyy-MM-dd") + " " + Time + ":00";
                    return dt;
                default:
                    DateTime dateTime;
                    if (DateTime.TryParse(condition, out dateTime))
                    {
                        var d = Convert.ToDateTime(condition).ToString("yyyy-MM-dd") + " " + Time + ":00";
                        dt = d;
                    }
                    else
                    {
                        dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                    }
                    //dt = DateTime.ParseExact(condition + " " + Time, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"); // DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " " + Time + ":00";

                    return dt;
            }
        }
    }

    public static class FormatCharacter
    {
        public static string InformixToCharFormat(string dformat)
        {
            try
            {
                if (dformat != "" && dformat != null)
                {
                    if (dformat.Contains("dd") || dformat.Contains("DD"))
                    {
                        dformat = dformat.Replace("dd", "%d");
                    }
                    if (dformat.Contains("MM"))
                    {
                        dformat = dformat.Replace("MM", "%m");
                    }
                    if (dformat.Contains("mm"))
                    {
                        dformat = dformat.Replace("mm", "%M");
                    }
                    if (dformat.Contains("yyyy") || dformat.Contains("YYYY") || dformat.Contains("YY") || dformat.Contains("yy"))
                    {
                        dformat = dformat.Replace("yyyy", "%Y");
                        dformat = dformat.Replace("YYYY", "%Y");
                        dformat = dformat.Replace("YY", "%Y");
                        dformat = dformat.Replace("yy", "%Y");
                    }
                    if (dformat.Contains("HH") || dformat.Contains("hh"))
                    {
                        dformat = dformat.Replace("HH", "%H");
                        dformat = dformat.Replace("hh", "%H");
                    }
                    if (dformat.Contains("ss") || dformat.Contains("SS"))
                    {
                        dformat = dformat.Replace("SS", "%S");
                        dformat = dformat.Replace("ss", "%S");
                    }
                }
                else
                {
                    dformat = "%d/%m/%Y %H:%M:%S";
                }
                //dd/MM/yyyy HH:mm:ss  %Y/%m/%d %H:%M:%S
            }
            catch
            {
                dformat = "%d/%m/%Y %H:%M:%S";
            }
            return dformat;
        }
    }


}
