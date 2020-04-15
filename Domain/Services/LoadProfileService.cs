using Domain.Common;
using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoadProfileService
    {
        Task<IEnumerable<Load>> GetDayWiseData(long MeterID, long Months, long Year, string param);
        Task<IEnumerable<ConsumptionCompare>> CompareDayWise(long MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear, string Parameter);
    }
    public class LoadProfileService : ILoadProfileService
    {
        etools_devEntities db;
        public LoadProfileService(DbContext db)
        {
            this.db = (etools_devEntities)db;
        }
        public async Task<IEnumerable<Load>> GetDayWiseData(long MeterID, long Months, long Year, string param)
        {
            String queryStr = String.Empty;

            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();

            string FromMnth = Months.ToString().Length == 1 ? "0" + Months.ToString() : Months.ToString();
            string ToMnth = (Months + 1).ToString().Length == 1 ? "0" + (Months + 1).ToString() : (Months + 1).ToString();
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

            DateTime dtFromDate;
            DateTime dtToDate;
            if (param.ToUpper() == "V" || param.ToUpper() == "I" || param.ToUpper() == "PF" || param.ToUpper() == "KW" || param.ToUpper() == "KVA")
            {
                dtFromDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm").AddMinutes(-15); //Not fetch the data from time series table
                dtToDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddMinutes(15); ;
            }
            else
            {
                dtFromDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
                dtToDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);
            }

            List<Load> loads = new List<Load>();
            List<Load_base> load_bases = new List<Load_base>();
            if (param.ToUpper() == "V")
            {
                sb.Clear();
                sb.Append("select table1.meterid,table1.tstamp as vrnTIME,table1.Vrn,table2.tstamp as vbnTime,table2.Vbn,table3.tstamp as vynTime,table3.Vyn FROM (");
                sb.Append("(SELECT meterid,tstamp,Vrn FROM (SELECT meterid,tstamp,nvl(Vrn,0) as Vrn, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY Vrn DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) >'" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid) table1");
                sb.Append("  inner join (SELECT meterid,tstamp,Vbn FROM (SELECT meterid,tstamp,nvl(Vbn,0) as Vbn, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY Vbn DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) > '" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid ) table2 ");
                sb.Append(" on table1.meterid=table2.meterid and (table1.tstamp::datetime year to day)=(table2.tstamp::datetime year to day) inner join ");
                sb.Append(" (SELECT meterid,tstamp,Vyn FROM (SELECT meterid,tstamp,nvl(Vyn,0) as Vyn, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY Vyn DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) > '" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid ) table3 ");
                sb.Append(" on table1.meterid=table3.meterid and (table1.tstamp::datetime year to day)= (table3.tstamp::datetime year to day))");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(dr["vrnTIME"]).Day.ToString(),
                            vrn = Math.Round(Convert.ToDouble(dr["Vrn"]), 2),
                            vyn = Math.Round(Convert.ToDouble(dr["Vbn"]), 2),
                            vbn = Math.Round(Convert.ToDouble(dr["Vyn"]), 2),
                            tsvrn = Convert.ToDateTime(dr["vrnTIME"]),
                            tsvyn = Convert.ToDateTime(dr["vbnTime"]),
                            tsvbn = Convert.ToDateTime(dr["vynTime"]),

                        });
                    }
                }
            }
            else if (param.ToUpper() == "I")
            {
                sb.Clear();
                sb.Append("select  table1.meterid,table1.tstamp as irtime ,table1.ir,table2.tstamp as iytime,table2.iy,table3.tstamp as ibtime,table3.ib FROM (");
                sb.Append("(SELECT meterid,tstamp,ir FROM (SELECT meterid,tstamp,nvl(ir,0) as ir, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY ir DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) >'" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid) table1");
                sb.Append("  inner join (SELECT meterid,tstamp,iy FROM (SELECT meterid,tstamp,nvl(iy,0) as iy, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY iy DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) > '" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid ) table2 ");
                sb.Append(" on table1.meterid=table2.meterid and (table1.tstamp::datetime year to day)=(table2.tstamp::datetime year to day) inner join ");
                sb.Append(" (SELECT meterid,tstamp,ib FROM (SELECT meterid,tstamp,nvl(ib,0) as ib, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY ib DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) > '" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid ) table3 ");
                sb.Append(" on table1.meterid=table3.meterid and (table1.tstamp::datetime year to day)= (table3.tstamp::datetime year to day))");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(dr["irtime"]).Day.ToString(),
                            ir = Math.Round(Convert.ToDouble(dr["ir"]), 2),
                            iy = Math.Round(Convert.ToDouble(dr["iy"]), 2),
                            ib = Math.Round(Convert.ToDouble(dr["ib"]), 2),
                            tsir = Convert.ToDateTime(dr["irtime"]),
                            tsib = Convert.ToDateTime(dr["iytime"]),
                            tsiy = Convert.ToDateTime(dr["ibtime"]),
                        });
                    }
                }
            }
            else if (param.ToUpper() == "KW")
            {
                sb.Clear();
                sb.Append("select table1.meterid,table1.tstamp as kwtime,table1.kw  FROM ");
                sb.Append("(select meterid,tstamp,kw FROM (select meterid,tstamp,nvl(kw,0) as kw, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY kw DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) >'" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid) table1");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(dr["kwtime"]).Day.ToString(),
                            kw = Math.Round(Convert.ToDouble(dr["kw"]), 2),
                            tskw = Convert.ToDateTime(dr["kwtime"])
                        });
                    }
                }
            }
            else if (param.ToUpper() == "PF")
            {

                sb.Clear();
                sb.Append("select table1.meterid,table1.tstamp as pftime ,table1.pf  FROM ");
                sb.Append("(select meterid,tstamp,pf FROM (select meterid,tstamp,nvl(pf,0) as pf, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY pf DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) >'" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid) table1");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(dr["pftime"]).Day.ToString(),
                            pf = Math.Round(Convert.ToDouble(dr["pf"]), 2),
                            tspf = Convert.ToDateTime(dr["pftime"])
                        });
                    }
                }
            }
            else if (param.ToUpper() == "KVA")
            {
                sb.Clear();
                sb.Append("select table1.meterid,table1.tstamp as kvatime ,table1.kva  FROM ");
                sb.Append("(select meterid,tstamp,kva FROM (select meterid,tstamp,nvl(kva,0) as kva, ROW_NUMBER() OVER (PARTITION BY DATE(tstamp),meterid ORDER BY kva DESC) AS rnum FROM LoadSurveyLogs where date(tstamp) >'" + dtFromDate.ToString("yyyy-MM-dd") + "::datetime year to day' and date(tstamp) < '" + dtToDate.ToString("yyyy-MM-dd") + "::datetime year to day' and meterid ='" + MeterID + "') WHERE rnum = 1 order by meterid) table1");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(dr["kvatime"]).Day.ToString(),
                            kVA = Math.Round(Convert.ToDouble(dr["kva"]), 2),
                            tskVA = Convert.ToDateTime(dr["kvatime"])
                        });
                    }
                }
            }
            else if (param.ToUpper() == "W")
            {
                List<long> MeterIds = new List<long>();

                string meterid = "";
                var Meter2 = (from d in db.instancedatas//db.instancedatas
                              where d.meterid == 2
                              select d).FirstOrDefault();

                var Meter4 = (from d in db.instancedatas//db.instancedatas
                              where d.meterid == 4
                              select d).FirstOrDefault();

                var Meter3 = (from d in db.instancedatas//db.instancedatas
                              where d.meterid == 3
                              select d).FirstOrDefault();

                var Meter5 = (from d in db.instancedatas//db.instancedatas
                              where d.meterid == 5
                              select d).FirstOrDefault();

                if (Meter2.tstamp.Value.AddSeconds(40) < Meter3.tstamp)
                {
                    meterid = meterid + "3,";
                    MeterIds.Add(Convert.ToInt64(3));
                }
                else
                {
                    meterid = meterid + "2,";
                    MeterIds.Add(Convert.ToInt64(2));
                }

                if (Meter4.tstamp.Value.AddSeconds(40) < Meter5.tstamp)
                {
                    meterid = meterid + "5,";
                    MeterIds.Add(Convert.ToInt64(5));
                }
                else
                {
                    meterid = meterid + "4,";
                    MeterIds.Add(Convert.ToInt64(4));
                }
                string Newmeterid = meterid.Remove(meterid.Length - 1);
                DateTime dtBlockNoDate = DateTime.Now;
                DateTime dtDateBlockFrom = new DateTime(dtBlockNoDate.Year, dtBlockNoDate.Month, dtBlockNoDate.Day);
                TimeSpan span = dtBlockNoDate - dtDateBlockFrom;
                double minutes = span.TotalMinutes;

                int iBlockNo = Convert.ToInt16(minutes) / 15;
                int datarow = iBlockNo + 1;
                sb.Clear();

                string date1 = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string date2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string date3 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:15:00";
                sb.Append(" select first " + datarow.ToString() + " sum(avg_mw) as avg_mw,max(avg_hz) as avg_hz,tstamp,blockno from loadsurveylogs  where meterid in (" + Newmeterid + ") and (tstamp>'" + date1 + "' and tstamp<'" + date3 + "')  group by tstamp,blockno order by tstamp desc");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);
                DataTable dt1 = new DataTable();
                dt.DefaultView.Sort = "tstamp ASC";
                dt1 = dt.DefaultView.ToTable();

                string a = "select blockno,sum( sgvalue) as sgvalue,app_date from dcsg where app_date>'" + date1 + "' and app_date<'" + date3 + "'group by app_date,blockno order by blockno ";

                DataTable dt2 = new DataTable();
                dt2 = await DatabaseHandler.Handler(a);
                DataTable dt3 = new DataTable();
                dt2.DefaultView.Sort = "blockno ASC";
                dt3 = dt2.DefaultView.ToTable();

                int count = 0;

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        decimal data = 0;

                        foreach (DataRow dr2 in dt3.Rows)
                        {
                            if (Convert.ToInt32((dr["blockno"]).ToString()) == Convert.ToInt32((dr2["blockno"]).ToString()))
                            {
                                data = Convert.ToDecimal((dr2["sgvalue"]).ToString());
                            }
                        }
                        if (Convert.ToInt32((dr["blockno"]).ToString()) <= datarow)
                        {
                            loads.Add(new Load
                            {
                                Day = (dr["blockno"]).ToString(),//dr["blockno"].ToString(),
                                ExBus = Convert.ToDouble(dr["avg_mw"]),
                                //ExBus =dr["avg_mw"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["avg_mw"]),
                                SG = Convert.ToDouble(data),
                                AvgHz = Convert.ToDouble(dr["avg_hz"]),
                                //SG =  dr["sgvalue"] == DBNull.Value ? (double?) null : Convert.ToDouble(dr["sgvalue"]),
                                tskw = Convert.ToDateTime(dr["tstamp"])
                            });
                        }
                        count++;
                    }
                }
            }
            else if (param.ToUpper() == "Z")
            {
                DateTime DCSGTime = DateTime.Now.AddDays(-1);
                DateTime DCSGTime1 = DateTime.Now.AddDays(1);
                DateTime dtBlockNoDate = DateTime.Now;
                DateTime dtDateBlockFrom = new DateTime(dtBlockNoDate.Year, dtBlockNoDate.Month, dtBlockNoDate.Day);
                TimeSpan span = dtBlockNoDate - dtDateBlockFrom;
                double minutes = span.TotalMinutes;
                int iBlockNo = Convert.ToInt16(minutes) / 15;
                string StartBlockTime = GetBlockInformation.getBlockTime(iBlockNo);
                string EndBlockTime = GetBlockInformation.getBlockTime(iBlockNo + 1);
                string date1 = DateTime.Now.ToString("yyyy-MM-dd") + " " + StartBlockTime + ":00";
                string date2 = DateTime.Now.ToString("yyyy-MM-dd") + " " + EndBlockTime + ":00";
                List<dcsg> aa = (from l in db.dcsgs
                                 where l.blockno == iBlockNo && (l.tstamp > DCSGTime && l.tstamp < DCSGTime1) //date added
                                 orderby l.tstamp ascending
                                 select l).ToList();

                decimal sgvalue = Convert.ToDecimal(aa.Sum(x => x.sgvalue));
                sb.Append(" select sum(cblk_avg_mw) as avg_mw,max(cblk_avg_hz) as avg_hz,tstamp,max(" + sgvalue + ") as sgvalue from instancedatalogs  where meterid in(2,4)and (tstamp>'" + date1 + "' and tstamp<'" + date2 + "')  group by tstamp order by tstamp asc;");

                queryStr = sb.ToString();
                sb.Clear();
                dt = await DatabaseHandler.Handler(queryStr);

                foreach (DataRow dr in dt.Rows)
                {
                    TimeSpan span1 = Convert.ToDateTime(dr["tstamp"]) - Convert.ToDateTime(date1);
                    loads.Add(new Load
                    {
                        Day = span1.Minutes.ToString(),//dr["blockno"].ToString(),
                        ExBus = Convert.ToDouble(dr["avg_mw"]),
                        //ExBus =dr["avg_mw"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["avg_mw"]),
                        SG = Convert.ToDouble(dr["sgvalue"]),
                        AvgHz = Convert.ToDouble(dr["avg_hz"]),
                        //SG =  dr["sgvalue"] == DBNull.Value ? (double?) null : Convert.ToDouble(dr["sgvalue"]),
                        tskw = Convert.ToDateTime(dr["tstamp"])
                    });
                }
            }
            else
            {
                var meterparameter = (from m in db.meters
                                      where m.id == MeterID
                                      select m.parameter).Single();
                if (meterparameter == "Kwh_Imp")
                {

                    string query = " select * from loadprofile where ts > '" + dtFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "and ts < '" + dtToDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                    dt = await DatabaseHandler.Handler(query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Load_base load_base = new Load_base();
                        load_base.date = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.kwh = Convert.ToDouble(dr["kwh_import"].ToString());
                        load_base.kVARh = Convert.ToDouble(dr["kvarh_lag_imp"].ToString());
                        load_base.kVA = Convert.ToDouble(dr["avg_kva"].ToString());
                        load_base.tskwh = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.tskVARh = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.tskVA = Convert.ToDateTime(dr["ts"].ToString());
                        load_bases.Add(load_base);
                    }

                    foreach (Load_base d in load_bases)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(),
                            kwh = Math.Round(Convert.ToDouble(d.kwh), 2),

                            kVARh = Math.Round(Convert.ToDouble(d.kVARh), 2),
                            kVA = Math.Round(Convert.ToDouble(d.kVA), 2),
                            tskwh = d.tskwh,

                            tskVARh = d.tskVARh,
                            tskVA = d.tskVA
                        });
                    }
                }
                else
                {
                    string query = " select * from loadprofile where ts > '" + dtFromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       "and ts < '" + dtToDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                    dt = await DatabaseHandler.Handler(query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Load_base load_base = new Load_base();
                        load_base.date = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.kwh = Convert.ToDouble(dr["kwh_export"].ToString());
                        load_base.kVARh = Convert.ToDouble(dr["kvarh_lag_imp"].ToString());
                        load_base.kVA = Convert.ToDouble(dr["avg_kva"].ToString());
                        load_base.tskwh = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.tskVARh = Convert.ToDateTime(dr["ts"].ToString());
                        load_base.tskVA = Convert.ToDateTime(dr["ts"].ToString());

                        load_bases.Add(load_base);
                    }

                    foreach (Load_base d in load_bases)
                    {
                        loads.Add(new Load
                        {
                            Day = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(),
                            kwh = Math.Round(Convert.ToDouble(d.kwh), 2),

                            kVARh = Math.Round(Convert.ToDouble(d.kVARh), 2),
                            kVA = Math.Round(Convert.ToDouble(d.kVA), 2),
                            tskwh = d.tskwh,
                            tskVARh = d.tskVARh,
                            tskVA = d.tskVA
                        });
                    }
                }
            }

            return loads.OrderBy(item => int.Parse(item.Day));
        }

        public async Task<IEnumerable<ConsumptionCompare>> CompareDayWise(long MeterID, long FromMonth, long FromYear, long ToMonth, long ToYear, string Parameter)
        {
            string FromMnth = FromMonth.ToString().Length == 1 ? "0" + FromMonth.ToString() : FromMonth.ToString();
            string ToFromMnth = FromMonth.ToString().Length == 1 ? "0" + (FromMonth + 1).ToString() : (FromMonth + 1).ToString();
            string strFromDate = "01/" + FromMnth + "/" + FromYear.ToString() + " 00:" + "00";
            string strToDate = "";
            if (ToFromMnth == "13")
            {
                strToDate = "01/" + "01" + "/" + (FromYear + 1).ToString() + " 00:" + "00";
            }
            else
            {
                strToDate = "01/" + ToFromMnth + "/" + FromYear.ToString() + " 00:" + "00";
            }
            DateTime dtFromCompareDate;
            DateTime dtToCompareDate;
            if (Parameter == "kwh_import")
            {
                dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
                dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);

            }
            else
            {
                dtFromCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm").AddDays(-1);
                dtToCompareDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm");
            }

            List<Consumption> lstConsumptionCompare = new List<Consumption>();
            List<Consumption> lstConsumptionCompareWith = new List<Consumption>();

            DataTable dt = new DataTable();

            if (Parameter == "kwh_import")
            {
                string query = " select * from loadprofile where ts > '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       "and ts < '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                dt = await DatabaseHandler.Handler(query);
                List<LoadProfile_base> loadProfile_bases = new List<LoadProfile_base>();

                foreach (DataRow dr in dt.Rows)
                {
                    LoadProfile_base loadProfile_base = new LoadProfile_base();
                    loadProfile_base.date = Convert.ToDateTime(dr["ts"].ToString());
                    loadProfile_base.vrn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vrn"].ToString()) ? "0.0" : dr["avg_vrn"].ToString());
                    loadProfile_base.vbn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vbn"].ToString()) ? "0.0" : dr["avg_vbn"].ToString());
                    loadProfile_base.vyn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vyn"].ToString()) ? "0.0" : dr["avg_vyn"].ToString());
                    loadProfile_base.ir = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ir"].ToString()) ? "0.0" : dr["avg_ir"].ToString());
                    loadProfile_base.iy = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_iy"].ToString()) ? "0.0" : dr["avg_iy"].ToString());
                    loadProfile_base.ib = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ib"].ToString()) ? "0.0" : dr["avg_ib"].ToString());
                    loadProfile_base.kwh_imp = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["kwh_import"].ToString()) ? "0.0" : dr["kwh_import"].ToString());
                    loadProfile_bases.Add(loadProfile_base);
                }

                foreach (LoadProfile_base d in loadProfile_bases)
                {
                    switch (Parameter)
                    {
                        case "ib":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ib), 2) });
                            break;
                        case "ir":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ir), 2) });
                            break;
                        case "iy":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.iy), 2) });
                            break;
                        case "kwh_import":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.kwh_imp), 2) });
                            break;
                        case "vbn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vbn), 2) });
                            break;
                        case "vrn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vrn), 2) });
                            break;
                        case "vyn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vyn), 2) });
                            break;
                    }
                }
            }
            else
            {
                string query = " select * from loadprofile where ts > '" + dtFromCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       "and ts < '" + dtToCompareDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                dt = await DatabaseHandler.Handler(query);
                List<LoadProfile_base> loadProfile_bases = new List<LoadProfile_base>();

                foreach (DataRow dr in dt.Rows)
                {
                    LoadProfile_base loadProfile_base = new LoadProfile_base();
                    loadProfile_base.date = Convert.ToDateTime(dr["ts"].ToString());                    
                    loadProfile_base.vrn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vrn"].ToString()) ? "0.0" : dr["avg_vrn"].ToString());
                    loadProfile_base.vbn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vbn"].ToString()) ? "0.0" : dr["avg_vbn"].ToString());
                    loadProfile_base.vyn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vyn"].ToString()) ? "0.0" : dr["avg_vyn"].ToString());
                    loadProfile_base.ir = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ir"].ToString()) ? "0.0" : dr["avg_ir"].ToString());
                    loadProfile_base.iy = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_iy"].ToString()) ? "0.0" : dr["avg_iy"].ToString());
                    loadProfile_base.ib = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ib"].ToString()) ? "0.0" : dr["avg_ib"].ToString());
                    loadProfile_base.kwh_exp = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["kwh_export"].ToString()) ? "0.0" : dr["kwh_export"].ToString());                   
                    loadProfile_bases.Add(loadProfile_base);
                }

                foreach (LoadProfile_base d in loadProfile_bases)
                {
                    switch (Parameter)
                    {
                        case "ib":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ib), 2) });
                            break;
                        case "ir":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ir), 2) });
                            break;
                        case "iy":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.iy), 2) });
                            break;
                        case "kwh_export":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.kwh_exp), 2) });
                            break;
                        case "vbn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vbn), 2) });
                            break;
                        case "vrn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vrn), 2) });
                            break;
                        case "vyn":
                            lstConsumptionCompare.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vyn), 2) });
                            break;
                    }
                }
            }

            //Compare With
            FromMnth = ToMonth.ToString().Length == 1 ? "0" + ToMonth.ToString() : ToMonth.ToString();
            ToFromMnth = ToMonth.ToString().Length == 1 ? "0" + (ToMonth + 1).ToString() : (ToMonth + 1).ToString();
            strFromDate = "01/" + FromMnth + "/" + ToYear.ToString() + " 00:" + "00";
            strToDate = "";
            if (ToFromMnth == "13")
            {
                strToDate = "01/" + "01" + "/" + (ToYear + 1).ToString() + " 00:" + "00";
            }
            else
            {
                strToDate = "01/" + ToFromMnth + "/" + ToYear.ToString() + " 00:" + "00";
            }

            DateTime dtFromCompareWithDate;
            DateTime dtToCompareWithDate;
            if (Parameter == "kwh_import")
            {
                dtFromCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm");
                dtToCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm").AddDays(1);
            }
            else
            {
                dtFromCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strFromDate, "dd/MM/yyyy HH:mm").AddDays(-1);
                dtToCompareWithDate = CMSDateTime.CMSDateTime.ConvertToDateTime(strToDate, "dd/MM/yyyy HH:mm");
            }

            if (Parameter == "kwh_import")
            {
                string query = " select * from loadprofile where ts > '" + dtFromCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       "and ts < '" + dtToCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                dt = await DatabaseHandler.Handler(query);
                List<LoadProfile_base> loadProfile_bases = new List<LoadProfile_base>();

                foreach (DataRow dr in dt.Rows)
                {
                    LoadProfile_base loadProfile_base = new LoadProfile_base();
                    loadProfile_base.date = Convert.ToDateTime(dr["ts"].ToString());
                    loadProfile_base.vrn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vrn"].ToString()) ? "0.0" : dr["avg_vrn"].ToString());
                    loadProfile_base.vbn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vbn"].ToString()) ? "0.0" : dr["avg_vbn"].ToString());
                    loadProfile_base.vyn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vyn"].ToString()) ? "0.0" : dr["avg_vyn"].ToString());
                    loadProfile_base.ir = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ir"].ToString()) ? "0.0" : dr["avg_ir"].ToString());
                    loadProfile_base.iy = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_iy"].ToString()) ? "0.0" : dr["avg_iy"].ToString());
                    loadProfile_base.ib = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ib"].ToString()) ? "0.0" : dr["avg_ib"].ToString());
                    loadProfile_base.kwh_imp = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["kwh_import"].ToString()) ? "0.0" : dr["kwh_import"].ToString());                 
                    loadProfile_bases.Add(loadProfile_base);
                }

                foreach (LoadProfile_base d in loadProfile_bases)
                {
                    switch (Parameter)
                    {
                        case "ib":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ib), 2) });
                            break;
                        case "ir":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ir), 2) });
                            break;
                        case "iy":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.iy), 2) });
                            break;
                        case "kwh_import":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.kwh_imp), 2) });
                            break;
                        case "vbn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vbn), 2) });
                            break;
                        case "vrn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vrn), 2) });
                            break;
                        case "vyn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vyn), 2) });
                            break;
                    }
                }
            }
            else
            {
                string query = " select * from loadprofile where ts > '" + dtFromCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       "and ts < '" + dtToCompareWithDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and meterid  =  '" + MeterID + "'";

                dt = await DatabaseHandler.Handler(query);
                List<LoadProfile_base> loadProfile_bases = new List<LoadProfile_base>();

                foreach (DataRow dr in dt.Rows)
                {
                    LoadProfile_base loadProfile_base = new LoadProfile_base();
                    loadProfile_base.date = Convert.ToDateTime(dr["ts"].ToString());
                    loadProfile_base.vrn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vrn"].ToString()) ? "0.0" : dr["avg_vrn"].ToString());
                    loadProfile_base.vbn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vbn"].ToString()) ? "0.0" : dr["avg_vbn"].ToString());
                    loadProfile_base.vyn = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_vyn"].ToString()) ? "0.0" : dr["avg_vyn"].ToString());
                    loadProfile_base.ir = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ir"].ToString()) ? "0.0" : dr["avg_ir"].ToString());
                    loadProfile_base.iy = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_iy"].ToString()) ? "0.0" : dr["avg_iy"].ToString());
                    loadProfile_base.ib = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["avg_ib"].ToString()) ? "0.0" : dr["avg_ib"].ToString());
                    loadProfile_base.kwh_exp = Convert.ToDouble(string.IsNullOrWhiteSpace(dr["kwh_export"].ToString()) ? "0.0" : dr["kwh_export"].ToString());
                    loadProfile_bases.Add(loadProfile_base);
                }

                foreach (LoadProfile_base d in loadProfile_bases)
                {
                    switch (Parameter)
                    {
                        case "ib":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ib), 2) });
                            break;
                        case "ir":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.ir), 2) });
                            break;
                        case "iy":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.iy), 2) });
                            break;
                        case "kwh_export":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).AddDays(-1).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.kwh_exp), 2) });
                            break;
                        case "vbn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vbn), 2) });
                            break;
                        case "vrn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vrn), 2) });
                            break;
                        case "vyn":
                            lstConsumptionCompareWith.Add(new Consumption { Name = Convert.ToDateTime(d.date).Day.ToString(), Unit = Math.Round(Convert.ToDouble(d.vyn), 2) });
                            break;
                    }
                }
            }

            List<ConsumptionCompare> lstDayWiseComparison = new List<ConsumptionCompare>();
            ConsumptionCompare objCompare = null;
            for (int i = 1; i <= 31; i++)
            {
                objCompare = new ConsumptionCompare();
                objCompare.Day = i.ToString();
                if (lstConsumptionCompare.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit1 = 0;
                }
                else
                {
                    if (lstConsumptionCompare.Find(c => c.Name == objCompare.Day) == null)
                    {
                        objCompare.Unit1 = 0;
                    }
                    else
                    {
                        objCompare.Unit1 = Convert.ToDouble(lstConsumptionCompare.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : lstConsumptionCompare.Find(c => c.Name == objCompare.Day).Unit;
                    }
                }
                if (lstConsumptionCompareWith.Find(c => c.Name == objCompare.Day) == null)
                {
                    objCompare.Unit2 = 0;
                }
                else
                {
                    if ((lstConsumptionCompareWith.Find(c => c.Name == objCompare.Day)) == null)
                    { objCompare.Unit2 = 0; }
                    else
                    {
                        objCompare.Unit2 = Convert.ToDouble(lstConsumptionCompareWith.Find(c => c.Name == objCompare.Day).Unit) == 0.0 ? 0 : lstConsumptionCompareWith.Find(c => c.Name == objCompare.Day).Unit;
                    }
                }
                lstDayWiseComparison.Add(objCompare);
            }
            return lstDayWiseComparison;
        }
    }
}
