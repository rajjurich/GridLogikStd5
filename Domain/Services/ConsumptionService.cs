using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Framework.Common;
using Domain.Common;
using System.Data;

namespace Domain.Services
{
    public interface IConsumptionService
    {
        Task<IEnumerable<Consumption>> GetMonthWiseData(string MeterID, long FromMonths, long FromYear, long ToMonths, long ToYear);
        Task<IEnumerable<Consumption>> GetDayWiseData(string MeterID, long Month, long Year);
        Task<IEnumerable<Consumption>> GetHourWiseData(string MeterID, long Day, long Month, long Year);
        Task<IEnumerable<ConsumptionCompare>> CompareBlockWise(string MeterID, string compareDate, string withDate);
        Task<IEnumerable<ConsumptionCompare>> CompareHourWise(string MeterID, string compareDate, string withDate);
        Task<IEnumerable<ConsumptionCompare>> CompareDayWise(string MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear);
        Task<IEnumerable<ConsumptionCompare>> CompareYearWise(string MeterID, long FromYear, long ToYear);
    }
    public class ConsumptionService : IConsumptionService
    {
        etools_devEntities db;
        string query = string.Empty;
        string column = string.Empty;
        string table = string.Empty;
        string ConsumptionLogic = string.Empty;
        DataTable dt = new DataTable();
        List<Consumption> Consumptions = new List<Consumption>();
        List<Consumption> ConsumptionsCompare = new List<Consumption>();
        List<Consumption1> Consumptions1 = new List<Consumption1>();
        public ConsumptionService(DbContext db)
        {
            this.db = (etools_devEntities)db;

            ConsumptionLogic = ConfigurationManager.AppSettings["ConsumptionLogic"];
            ConsumptionLogic = String.IsNullOrEmpty(ConsumptionLogic) ? "0" : ConsumptionLogic;
        }
        public async Task<IEnumerable<Consumption>> GetMonthWiseData(string MeterID, long FromMonths, long FromYear, long ToMonths, long ToYear)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();
            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            string FromMnth = FromMonths.ToString().Length == 1 ? "0" + FromMonths.ToString() : FromMonths.ToString();
            string ToMnth = ToMonths.ToString().Length == 1 ? "0" + ToMonths.ToString() : ToMonths.ToString();
            string strFromDate = "01/" + FromMnth + "/" + FromYear.ToString() + " 00:" + "00";
            string strToDate = "01/" + ToMnth + "/" + ToYear.ToString() + " 00:" + "00";

            DateTime dtFromDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
            DateTime dtToDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddMonths(2);

            if (ConsumptionLogic == "1")
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_monthwise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conexp_monthwise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_monthwise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_monthwise";
                }
            }

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
                            " i join meters b on b.id=i.meterid where i.ts > '" + dtFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                            " i.ts < '" + dtToDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                            " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                if (Convert.ToDateTime(d.Name).Month == 1)
                {
                    Consumptions.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).AddMonths(-1).Month).GetShortMonthName() + "-" + Convert.ToDateTime(d.Name).AddYears(-1).Year, Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
                }
                else
                {
                    Consumptions.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).Month - 1).GetShortMonthName() + "-" + Convert.ToDateTime(d.Name).Year, Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
                }
            }
            return Consumptions;
        }

        public async Task<IEnumerable<Consumption>> GetDayWiseData(string MeterID, long Month, long Year)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            string FromMnth = Month.ToString().Length == 1 ? "0" + Month.ToString() : Month.ToString();
            string ToMnth = Month.ToString().Length == 1 ? ((Month + 1).ToString().Length == 1 ? "0" + (Month + 1).ToString() : (Month + 1).ToString()) : (Month + 1).ToString();
            string strFromDate = "01/" + FromMnth + "/" + Year.ToString() + " 00:" + "00";
            string strToDate = "";
            if (ToMnth == "13")
            {
                strToDate = "01/" + "01" + "/" + (Year + 1).ToString() + " 00:" + "00";
            }
            else
            {
                strToDate = "01/" + ToMnth + "/" + Year.ToString() + " 00:" + "00";
            }

            DateTime dtFromDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
            DateTime dtToDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);

            if (ConsumptionLogic == "1")
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_daywise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conimp_daywise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_daywise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_daywise";
                }
            }

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
                           " i join meters b on b.id=i.meterid where i.ts > '" + dtFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                           " i.ts < '" + dtToDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                           " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                Consumptions.Add(new Consumption { Name = Convert.ToDateTime(d.Name).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
            }
            return Consumptions;
        }

        public async Task<IEnumerable<Consumption>> GetHourWiseData(string MeterID, long Day, long Month, long Year)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            string FromDay = Day.ToString().Length == 1 ? "0" + Day.ToString() : Day.ToString();
            string FromMnth = Month.ToString().Length == 1 ? "0" + Month.ToString() : Month.ToString();
            string strFromDate = FromDay + "/" + FromMnth + "/" + Year.ToString() + " 00:" + "00";

            DateTime dtFromDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
            DateTime dtToDate = dtFromDate.AddDays(1);

            if (ConsumptionLogic == "1")
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_hourwise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conexp_hourwise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_hourwise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_hourwise";
                }
            }

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
                           " i join meters b on b.id=i.meterid where i.ts > '" + dtFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                           " i.ts < '" + dtToDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                           " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                Consumptions.Add(new Consumption { Name = Convert.ToInt16(Math.Round(Convert.ToDecimal(Convert.ToDateTime(d.Name).Hour))).ToString() == "0" ? "24" : Convert.ToInt16(Math.Round(Convert.ToDecimal(Convert.ToDateTime(d.Name).Hour))).ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
            }
            return Consumptions;

        }

        public async Task<IEnumerable<ConsumptionCompare>> CompareBlockWise(string MeterID, string CompareDate, string withDate)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            CompareDate = CompareDate.Replace("-", "/");
            withDate = withDate.Replace("-", "/");
            CompareDate = CompareDate + " 00:00";
            withDate = withDate + " 00:00";
            TimeSpan tspanStart = new TimeSpan(0, 0, 0);
            TimeSpan tspanEnd = new TimeSpan(23, 59, 59);

            DateTime dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(CompareDate, "dd/MM/yyyy hh:mm").Add(tspanStart);
            DateTime dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(CompareDate, "dd/MM/yyyy hh:mm").Add(tspanEnd).AddMinutes(15);

            if (meterparameter.ToLower() == "kwh_imp")
            {
                column = "kwh_imp * ctprimary";
                table = "loadsurveylogs";
            }
            else
            {
                column = "kwh_exp * ctprimary";
                table = "loadsurveylogs";
            }

            query = " select sum(" + column + ") as Unit,tstamp as Name from " + table + "" +
                        " i join meters b on b.id=i.meterid where i.tstamp >= '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                        " i.tstamp <= '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " and i.meterid in (" + meters + ") group by i.tstamp order by i.tstamp;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            if (Consumptions1.Count > 1)
            {
                for (int i = 1; i < Consumptions1.Count; i++)
                {
                    double consumption = Convert.ToDouble(Consumptions1[i - 1].Unit);
                    if (Consumptions1[i].Unit != 0)
                    {
                        consumption = Convert.ToDouble(Consumptions1[i].Unit) - consumption;
                        Consumptions.Add(new Consumption { Name = Convert.ToDateTime(Consumptions1[i].Name).ToString(), Unit = Math.Round(Convert.ToDouble(consumption), 2) });
                    }
                    else
                    {
                        Consumptions.Add(new Consumption { Name = Convert.ToDateTime(Consumptions1[i].Name).ToString(), Unit = 0 });
                    }
                }
            }

            Consumptions1.Clear();

            dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(withDate, "dd/MM/yyyy hh:mm").Add(tspanStart);
            dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(withDate, "dd/MM/yyyy hh:mm").Add(tspanEnd).AddMinutes(15);

            query = " select sum(" + column + ") as Unit,tstamp as Name from " + table + "" +
                    " i join meters b on b.id=i.meterid where i.tstamp >= '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                    " i.tstamp <= '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " and i.meterid in (" + meters + ") group by i.tstamp order by i.tstamp;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            if (Consumptions1.Count > 1)
            {
                for (int i = 1; i < Consumptions1.Count; i++)
                {
                    double consumption = Convert.ToDouble(Consumptions1[0].Unit);
                    if (Consumptions1[i].Unit != 0)
                    {
                        consumption = Convert.ToDouble(Consumptions1[i].Unit) - Convert.ToDouble(Consumptions1[i - 1].Unit);
                    }
                    else
                    {
                        consumption = Convert.ToDouble(Consumptions1[i].Unit);
                    }
                    ConsumptionsCompare.Add(new Consumption { Name = Convert.ToDateTime(Consumptions1[i].Name).ToString(), Unit = Math.Round(Convert.ToDouble(consumption), 2) });
                }
            }

            dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(CompareDate, "dd/MM/yyyy HH:mm").Add(tspanStart);
            dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(withDate, "dd/MM/yyyy HH:mm").Add(tspanStart);

            List<ConsumptionCompare> ConsumptionCompareBlockWise = new List<ConsumptionCompare>();

            for (int i = 0; i < 96; i++)
            {
                ConsumptionCompare objCompare = new ConsumptionCompare();
                objCompare.Day = (i + 1).ToString();
                dtFromCompareDate = dtFromCompareDate.AddMinutes(15);
                dtToCompareDate = dtToCompareDate.AddMinutes(15);
                if (Consumptions.Find(c => c.Name == dtFromCompareDate.ToString()) == null)
                {
                    objCompare.Unit1 = 0;
                }
                else
                {
                    objCompare.Unit1 = Math.Round(Convert.ToDouble(Consumptions.Find(c => c.Name == dtFromCompareDate.ToString()).Unit) == 0.0 ? 0 : Consumptions.Find(c => c.Name == dtFromCompareDate.ToString()).Unit, 5);
                }
                if (ConsumptionsCompare.Find(c => c.Name == dtToCompareDate.ToString()) == null)
                {
                    objCompare.Unit2 = 0;
                }
                else
                {
                    objCompare.Unit2 = Math.Round(Convert.ToDouble(ConsumptionsCompare.Find(c => c.Name == dtToCompareDate.ToString()).Unit) == 0.0 ? 0 : ConsumptionsCompare.Find(c => c.Name == dtToCompareDate.ToString()).Unit, 5);
                }
                ConsumptionCompareBlockWise.Add(objCompare);
            }
            return ConsumptionCompareBlockWise;
        }

        public async Task<IEnumerable<ConsumptionCompare>> CompareHourWise(string MeterID, string CompareDate, string withDate)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            TimeSpan tspanStart = new TimeSpan(0, 0, 0);
            TimeSpan tspanEnd = new TimeSpan(23, 59, 59);

            DateTime dtFromCompareDate = Convert.ToDateTime(CompareDate).Add(tspanStart);
            DateTime dtToCompareDate = Convert.ToDateTime(CompareDate).Add(tspanEnd);

            if (ConsumptionLogic == "0")//newcode
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_hourwise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_hourwise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_hourwise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conexp_hourwise";
                }
            }

            query = " select " + column + " as Unit,ts as Name from " + table + "" +
          " i where i.ts >= '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
          " i.ts <= '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
          " and i.meterid in (" + meters + ") order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                Consumptions.Add(new Consumption { Name = Convert.ToDateTime(d.Name).Hour.ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
            }

            DateTime dtFromWithDate = Convert.ToDateTime(withDate).Add(tspanStart);
            DateTime dtToWithDate = Convert.ToDateTime(withDate).Add(tspanEnd);

            query = " select " + column + " as Unit,ts as Name from " + table + "" +
       " i where i.ts >= '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
       " i.ts <= '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
       " and i.meterid in (" + meters + ") order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                ConsumptionsCompare.Add(new Consumption { Name = Convert.ToDateTime(d.Name).Hour.ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
            }

            List<ConsumptionCompare> ConsumptionCompareHourWise = new List<ConsumptionCompare>();
            ConsumptionCompare objCompare = null;
            for (int i = 0; i < 24; i++)
            {
                objCompare = new ConsumptionCompare();
                objCompare.Day = i.ToString();
                if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit1 = 0;
                }
                else
                {
                    if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                    {
                        objCompare.Unit1 = 0;
                    }
                    else
                    {
                        objCompare.Unit1 = Math.Round(Convert.ToDouble(Consumptions.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : Consumptions.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                if (ConsumptionsCompare.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit2 = 0;
                }
                else
                {
                    if ((ConsumptionsCompare.Find(c => c.Name == objCompare.Day)) == null)
                    { objCompare.Unit2 = 0; }
                    else
                    {
                        objCompare.Unit2 = Math.Round(Convert.ToDouble(ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                ConsumptionCompareHourWise.Add(objCompare);
            }
            return ConsumptionCompareHourWise;
        }

        public async Task<IEnumerable<ConsumptionCompare>> CompareDayWise(string MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();
            //Compare
            string FromMnth = FromMonth.ToString().Length == 1 ? "0" + FromMonth.ToString() : FromMonth.ToString();
            string ToMnth = FromMonth.ToString().Length == 1 ? "0" + (FromMonth + 1).ToString() : (FromMonth + 1).ToString();
            string strFromDate = "01/" + FromMnth + "/" + FromYear.ToString() + " 00:" + "00";
            string strToDate = "";
            if (ToMnth == "13")
            {
                strToDate = "01/" + "01" + "/" + (FromYear + 1).ToString() + " 00:" + "00";
            }
            else
            {
                strToDate = "01/" + ToMnth + "/" + FromYear.ToString() + " 00:" + "00";
            }

            DateTime dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
            DateTime dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);

            if (ConsumptionLogic == "0")//newcode
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_daywise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_daywise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_daywise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conexp_daywise";
                }
            }

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
                       " i join meters b on b.id=i.meterid where i.ts > '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                       " i.ts < '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                Consumptions.Add(new Consumption { Name = Convert.ToDateTime(d.Name).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
            }

            FromMnth = ToMonth.ToString().Length == 1 ? "0" + ToMonth.ToString() : ToMonth.ToString();
            ToMnth = ToMonth.ToString().Length == 1 ? "0" + (ToMonth + 1).ToString() : (ToMonth + 1).ToString();
            strFromDate = "01/" + FromMnth + "/" + ToYear.ToString() + " 00:" + "00";
            strToDate = "";
            if (ToMnth == "13")
            {
                strToDate = "01/" + "01" + "/" + (ToYear + 1).ToString() + " 00:" + "00";
            }
            else
            {
                strToDate = "01/" + ToMnth + "/" + ToYear.ToString() + " 00:" + "00";
            }

            DateTime dtFromCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
            DateTime dtToCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);

            Consumptions1.Clear();

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
              " i join meters b on b.id=i.meterid where i.ts > '" + dtFromCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
              " i.ts < '" + dtToCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
              " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                ConsumptionsCompare.Add(new Consumption { Name = Convert.ToDateTime(d.Name).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.Unit), 5) });
            }

            List<ConsumptionCompare> ConsumptionCompareDayWise = new List<ConsumptionCompare>();
            ConsumptionCompare objCompare = null;

            for (int i = 1; i <= 31; i++)
            {
                objCompare = new ConsumptionCompare();
                objCompare.Day = i.ToString();
                if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit1 = 0;
                }
                else
                {
                    if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                    {
                        objCompare.Unit1 = 0;
                    }
                    else
                    {
                        objCompare.Unit1 = Math.Round(Convert.ToDouble(Consumptions.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : Consumptions.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                if (ConsumptionsCompare.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit2 = 0;
                }
                else
                {
                    if ((ConsumptionsCompare.Find(c => c.Name == objCompare.Day)) == null)
                    { objCompare.Unit2 = 0; }
                    else
                    {
                        objCompare.Unit2 = Math.Round(Convert.ToDouble(ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                ConsumptionCompareDayWise.Add(objCompare);
            }
            return ConsumptionCompareDayWise;
        }

        public async Task<IEnumerable<ConsumptionCompare>> CompareYearWise(string MeterID, long FromYear, long ToYear)
        {
            List<long?> TagIds = MeterID.Split('^').Select(x => (long?)Convert.ToInt64(x)).ToList();

            var meters = MeterID.Replace('^', ',');

            var meterparameter = (from m in db.meters
                                  where TagIds.Contains(m.id)
                                  select m.parameter).FirstOrDefault();

            string strFromDate = "01/12/" + FromYear.ToString() + " 00:" + "00";
            string strToDate = "01/01/" + FromYear.ToString() + " 23:" + "59";

            DateTime dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm").AddYears(-1).AddMonths(1);
            DateTime dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddYears(1).AddMonths(1);

            if (ConsumptionLogic == "0")
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "kwh_import";
                    table = "evt_monthwise";
                }
                else
                {
                    column = "kwh_export";
                    table = "evt_monthwise";
                }
            }
            else
            {
                if (meterparameter == "Kwh_Imp")
                {
                    column = "cum_kwh";
                    table = "evt_conimp_monthwise";
                }
                else
                {
                    column = "cum_kwh";
                    table = "evt_conexp_monthwise";
                }
            }

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
                     " i join meters b on b.id=i.meterid where i.ts > '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                     " i.ts < '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                     " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                if (Convert.ToDateTime(d.Name).Month == 1)
                {                
                    Consumptions.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).AddMonths(-1).Month - 1).GetShortMonthName(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
                }
                else
                {                 
                    Consumptions.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).Month - 1).GetShortMonthName(), Unit = Math.Round(Convert.ToDouble(d.Unit), 2) });
                }
                
            }

            strFromDate = "01/12/" + ToYear.ToString() + " 00:" + "00";
            strToDate = "01/01/" + ToYear.ToString() + " 23:" + "59";

            DateTime dtFromCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm").AddYears(-1).AddMonths(1);
            DateTime dtToCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddYears(1).AddMonths(1);

            Consumptions1.Clear();

            query = " select sum(" + column + ") as Unit,ts as Name from " + table + "" +
             " i join meters b on b.id=i.meterid where i.ts > '" + dtFromCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
             " i.ts < '" + dtToCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
             " and i.meterid in (" + meters + ") group by i.ts order by i.ts;";

            dt = await DatabaseHandler.Handler(query);

            foreach (DataRow dr in dt.Rows)
            {
                Consumption1 consumption1 = new Consumption1();
                consumption1.Name = Convert.ToDateTime(dr["Name"].ToString());
                consumption1.Unit = Convert.ToDouble(dr["Unit"].ToString());
                Consumptions1.Add(consumption1);
            }

            foreach (Consumption1 d in Consumptions1)
            {
                if (Convert.ToDateTime(d.Name).Month == 1)
                {                
                    ConsumptionsCompare.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).AddMonths(-1).Month - 1).GetShortMonthName(), Unit = Math.Round(Convert.ToDouble(d.Unit), 5) });
                }
                else
                {                 
                    ConsumptionsCompare.Add(new Consumption { Name = (Convert.ToDateTime(d.Name).Month - 1).GetShortMonthName(), Unit = Math.Round(Convert.ToDouble(d.Unit), 5) });
                }
            }

            List<ConsumptionCompare> ConsumptionCompareYearWise = new List<ConsumptionCompare>();
            ConsumptionCompare objCompare = null;
            for (int i = 1; i <= 12; i++)
            {
                objCompare = new ConsumptionCompare();
                objCompare.Day = (i).GetShortMonthName();
                if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit1 = 0;
                }
                else
                {
                    if (Consumptions.Find(c => c.Name == objCompare.Day) == null)
                    {
                        objCompare.Unit1 = 0;
                    }
                    else
                    {
                        objCompare.Unit1 = Math.Round(Convert.ToDouble(Consumptions.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : Consumptions.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                if (ConsumptionsCompare.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit2 = 0;
                }
                else
                {
                    if ((ConsumptionsCompare.Find(c => c.Name == objCompare.Day)) == null)
                    { objCompare.Unit2 = 0; }
                    else
                    {
                        objCompare.Unit2 = Math.Round(Convert.ToDouble(ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : ConsumptionsCompare.Find(c => c.Name == objCompare.Day).Unit, 5);
                    }
                }
                ConsumptionCompareYearWise.Add(objCompare);
            }
            return ConsumptionCompareYearWise;
        }
    }
}
