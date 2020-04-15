using Domain.Common;
using Domain.Entities;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Domain.Services
{
    public interface ILoadSurveyService
    {
        List<LoadService> GetMeterviewenergy(clsMeterviewModel model, string condition);
        List<LoadService> GetMeterviewprofile(clsMeterviewModel model, string condition);
        List<LoadService> GetMeterviewconsumption(clsMeterviewModel model, string condition);
        List<LoadService> GetMeterviewconsumptionExportImport(clsMeterviewModel model, string condition);
        List<LoadService> GetProfileLog(ProfileLogViewModel model);
        List<LoadService> GetEnergyLog(EnergyLogViewModel model);
        Task<DataTable> GetCommonData(CommonView model, string reportDisplayDate, string DateFormat);
    }
    public class LoadSurveyService : ILoadSurveyService
    {
        etools_devEntities db;
        IPrmGlobalService prmGlobalService;
        static string RptDispalyFormat = string.Empty;
        public LoadSurveyService(DbContext db, IPrmGlobalService prmGlobalService)
        {
            this.db = (etools_devEntities)db;
            this.prmGlobalService = prmGlobalService;

            RptDispalyFormat = prmGlobalService.FindBy(prm => (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "REPORT_DISPLAY" && prm.prmidentifier.ToUpper() == "FORMAT")).Select(prm => prm.prmvalue).FirstOrDefault();
        }
        public List<LoadService> GetMeterviewenergy(clsMeterviewModel model, string condition)
        {
            List<LoadService> loadService = new List<LoadService>();

            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            if (model.StartTime == null || model.StartTime == "")
                model.StartTime = "12:00 AM";
            if (model.EndTime == null || model.EndTime == "")
                model.EndTime = "11:45 PM";

            if (model.FromDate != null)
            {
                fromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                fromDate = Convert.ToDateTime(fromDate).AddMinutes(-15);
            }
            if (model.ToDate != null)
            {
                if (model.ToDate != null)
                {
                    if (model.EndTime != "11:45 PM")
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(15);
                    }
                    else
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(30);
                    }
                }
            }
            loadService = (from m in db.loadsurveylogs
                           join mtr in db.meters on m.meterid equals mtr.id
                           where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > fromDate && m.tstamp < toDate
                           select new
                           {
                               Meter_Name = mtr.metername,
                               METERID = mtr.id,
                               Tstamp = (DateTime)m.tstamp,
                               kWh_Exp = m.kwh_exp,
                               kWh_Imp = m.kwh_imp,
                               kVAh_Exp = m.kvah_exp,
                               kVAh_Imp = m.kvah_imp,
                               kVARh_Lag_Imp = m.kvarh_lag_imp,
                               kVARh_Lead_Imp = m.kvarh_lead_imp,
                               kVARh_Lag_Exp = m.kvarh_lag_exp,
                               KVARh_Lead_exp = m.kvarh_lead_exp,
                               kvarh_exp_103 = m.kvarh_exp_103,
                               kvarh_exp_97 = m.kvarh_exp_97,
                               kvarh_imp_103 = m.kvarh_imp_103,
                               kvarh_imp_97 = m.kvarh_imp_97,
                               day_kwh_exp = m.day_kwh_exp,
                               day_kwh_imp = m.day_kwh_imp,
                               cblk_kwh_exp = m.cblk_kwh_exp,
                               cblk_kwh_imp = m.cblk_kwh_imp,
                               avg_hz = m.avg_hz,
                               avg_mw = m.avg_mw
                           }).OrderBy(x => x.Tstamp).ToList()
                           .Select(x => new LoadService()
                           {
                               Meter_Name = x.Meter_Name,
                               METERID = x.METERID,
                               Date = x.Tstamp.ToString(RptDispalyFormat),
                               kWh_Exp = x.kWh_Exp,
                               kWh_Imp = x.kWh_Imp,
                               kVAh_Exp = x.kVAh_Exp,
                               kVAh_Imp = x.kVAh_Imp,
                               kVARh_Lag_Imp = x.kVARh_Lag_Imp,
                               kVARh_Lead_Imp = x.kVARh_Lead_Imp,
                               kVARh_Lag_Exp = x.kVARh_Lag_Exp,
                               KVARh_Lead_exp = x.KVARh_Lead_exp,
                               kvarh_exp_103 = x.kvarh_exp_103,
                               kvarh_exp_97 = x.kvarh_exp_97,
                               kvarh_imp_103 = x.kvarh_imp_103,
                               kvarh_imp_97 = x.kvarh_imp_97,
                               day_kwh_exp = x.day_kwh_exp,
                               day_kwh_imp = x.day_kwh_imp,
                               cblk_kwh_exp = x.cblk_kwh_exp,
                               cblk_kwh_imp = x.cblk_kwh_imp,
                               avg_hz = x.avg_hz,
                               avg_mw = x.avg_mw
                           }).ToList();

            return GetFilterData(Convert.ToString(model.Interval.ToUpper()), loadService);
        }

        public List<LoadService> GetMeterviewprofile(clsMeterviewModel model, string condition)
        {
            List<LoadService> loadService = new List<LoadService>();

            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            if (model.StartTime == null || model.StartTime == "")
                model.StartTime = "12:00 AM";
            if (model.EndTime == null || model.EndTime == "")
                model.EndTime = "11:45 PM";

            if (model.FromDate != null)
            {
                fromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                fromDate = Convert.ToDateTime(fromDate).AddMinutes(-15);
            }
            if (model.ToDate != null)
            {
                if (model.ToDate != null)
                {
                    if (model.EndTime != "11:45 PM")
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(15);
                    }
                    else
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(30);
                    }
                }
            }
            loadService = (from m in db.loadsurveylogs
                           join mtr in db.meters on m.meterid equals mtr.id
                           where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > fromDate && m.tstamp < toDate
                           select new LoadService
                           {
                               Meter_Name = mtr.metername,
                               METERID = mtr.id,
                               Tstamp = m.tstamp,
                               vll = m.vll,
                               i = m.i,
                               pf = m.pf,
                               kw = m.kw,
                               kvar = m.kvar,
                               kva = m.kva,
                               hz = m.hz,
                               vry = m.vry,
                               vyb = m.vyb,
                               vbr = m.vbr,
                               vrn = m.vrn,
                               vyn = m.vyn,
                               vbn = m.vbn,
                               vln = m.vln,
                               ir = m.ir,
                               iy = m.iy,
                               ib = m.ib,
                               kwr = m.kwr,
                               kwy = m.kwy,
                               kwb = m.kwb,
                               kva_r = m.kva_r,
                               kvay = m.kvay,
                               kvab = m.kvab,
                               kw_demand = m.kw_demand,
                               kva_demand = m.kva_demand
                           }).OrderBy(x => x.Tstamp).ToList()
                            .Select(x => new LoadService()
                            {
                                Meter_Name = x.Meter_Name,
                                METERID = x.METERID,
                                Date = x.Tstamp.Value.ToString(RptDispalyFormat),
                                vll = x.vll == null ? x.vll : Math.Round(Convert.ToDouble(x.vll), 2),
                                i = x.i == null ? x.i : Math.Round(Convert.ToDouble(x.i), 2),
                                pf = x.pf == null ? x.pf : Math.Round(Convert.ToDouble(x.pf), 3),

                                kw = x.kw == null ? x.kw : Math.Round(Convert.ToDouble(x.kw), 2),

                                kvar = x.kvar == null ? x.kvar : Math.Round(Convert.ToDouble(x.kvar), 2),

                                kva = x.kva == null ? x.kva : Math.Round(Convert.ToDouble(x.kva), 2),

                                hz = x.hz == null ? x.hz : Math.Round(Convert.ToDouble(x.hz), 2),

                                vry = x.vry == null ? x.vry : Math.Round(Convert.ToDouble(x.vry), 2),

                                vyb = x.vyb == null ? x.vyb : Math.Round(Convert.ToDouble(x.vyb), 2),

                                vbr = x.vbr == null ? x.vbr : Math.Round(Convert.ToDouble(x.vbr), 2),

                                vrn = x.vrn == null ? x.vrn : Math.Round(Convert.ToDouble(x.vrn), 2),

                                vyn = x.vyn == null ? x.vyn : Math.Round(Convert.ToDouble(x.vyn), 2),

                                vbn = x.vbn == null ? x.vbn : Math.Round(Convert.ToDouble(x.vbn), 2),

                                vln = x.vln == null ? x.vln : Math.Round(Convert.ToDouble(x.vln), 2),

                                ir = x.ir == null ? x.ir : Math.Round(Convert.ToDouble(x.ir), 2),

                                iy = x.iy == null ? x.iy : Math.Round(Convert.ToDouble(x.iy), 2),

                                ib = x.ib == null ? x.ib : Math.Round(Convert.ToDouble(x.ib), 2),

                                kwr = x.kwr == null ? x.kwr : Math.Round(Convert.ToDouble(x.kwr), 2),

                                kwy = x.kwy == null ? x.kwy : Math.Round(Convert.ToDouble(x.kwy), 2),

                                kwb = x.kwb == null ? x.kwb : Math.Round(Convert.ToDouble(x.kwb), 2),

                                kva_r = x.kva_r == null ? x.kva_r : Math.Round(Convert.ToDouble(x.kva_r), 2),

                                kvay = x.kvay == null ? x.kvay : Math.Round(Convert.ToDouble(x.kvay), 2),

                                kvab = x.kvab == null ? x.kvab : Math.Round(Convert.ToDouble(x.kvab), 2),

                                kw_demand = x.kw_demand == null ? x.kw_demand : Math.Round(Convert.ToDouble(x.kw_demand), 2),

                                kva_demand = x.kva_demand == null ? x.kva_demand : Math.Round(Convert.ToDouble(x.kva_demand), 2),
                            }).ToList();

            return GetFilterData(Convert.ToString(model.Interval.ToUpper()), loadService);
        }

        public List<LoadService> GetMeterviewconsumption(clsMeterviewModel model, string condition)
        {

            List<LoadService> loadService = new List<LoadService>();


            //DateTime fromDate = new DateTime();
            //DateTime toDate = new DateTime();
            //if (model.StartTime == null || model.StartTime == "")
            //    model.StartTime = "12:00 AM";
            //if (model.EndTime == null || model.EndTime == "")
            //    model.EndTime = "11:45 PM";

            //if (model.FromDate != null)
            //{
            //    fromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //    fromDate = Convert.ToDateTime(fromDate).AddMinutes(-15);
            //}
            //if (model.ToDate != null)
            //{
            //    if (model.EndTime != "11:45 PM")
            //    {
            //        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //        toDate = Convert.ToDateTime(toDate).AddMinutes(15);
            //    }
            //    else
            //    {
            //        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //        toDate = Convert.ToDateTime(toDate).AddMinutes(30);
            //    }
            //}

            //string ConsumptionLogic = ConfigurationManager.AppSettings["ConsumptionLogic"];
            //ConsumptionLogic = String.IsNullOrEmpty(ConsumptionLogic) ? "0" : ConsumptionLogic;


            //if (ConsumptionLogic == "0")
            //{

            //    loadService = (from m in db.evt_mquery_consumption_b
            //                   join mtr in db.meters on m.meterid equals mtr.id
            //                   where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > fromDate && m.tstamp < toDate
            //                   select new
            //                   {
            //                       Meter_Name = mtr.metername,
            //                       METERID = mtr.id,
            //                       Tstamp = (DateTime)m.tstamp,
            //                       kWh_Exp = m.kwh_exp,
            //                       kWh_Imp = m.kwh_imp,
            //                       kVAh_Exp = m.kvah_exp,
            //                       kVAh_Imp = m.kvah_imp,
            //                       kVARh_Lag_Imp = m.kvarh_lag_imp,
            //                       kVARh_Lead_Imp = m.kvarh_lead_imp,
            //                       kVARh_Lag_Exp = m.kvarh_lag_exp,
            //                       KVARh_Lead_exp = m.kvarh_lead_exp
            //                   }).OrderBy(x => x.Tstamp).ToList()
            //                   .Select(x => new LoadService()
            //                   {
            //                       Meter_Name = x.Meter_Name,
            //                       METERID = x.METERID,
            //                       Date = x.Tstamp.ToString(RptDispalyFormat),
            //                       kWh_Exp = x.kWh_Exp,
            //                       kWh_Imp = x.kWh_Imp,
            //                       kVAh_Exp = x.kVAh_Exp,
            //                       kVAh_Imp = x.kVAh_Imp,
            //                       kVARh_Lag_Imp = x.kVARh_Lag_Imp,
            //                       kVARh_Lead_Imp = x.kVARh_Lead_Imp,
            //                       kVARh_Lag_Exp = x.kVARh_Lag_Exp,
            //                       KVARh_Lead_exp = x.KVARh_Lead_exp
            //                   }).ToList();


            //}
            //else
            //{


            //    loadService = (from m in db.evt_mquery_consumption_b
            //                   join mtr in db.meters on m.meterid equals mtr.id
            //                   where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > fromDate && m.tstamp < toDate
            //                   select new
            //                   {
            //                       Meter_Name = mtr.metername,
            //                       METERID = mtr.id,
            //                       Tstamp = (DateTime)m.tstamp,
            //                       kWh_Exp = m.kwh_exp,
            //                       kWh_Imp = m.kwh_imp,
            //                       kVAh_Exp = m.kvah_exp,
            //                       kVAh_Imp = m.kvah_imp,
            //                       kVARh_Lag_Imp = m.kvarh_lag_imp,
            //                       kVARh_Lead_Imp = m.kvarh_lead_imp,
            //                       kVARh_Lag_Exp = m.kvarh_lag_exp,
            //                       KVARh_Lead_exp = m.kvarh_lead_exp
            //                   }).OrderBy(x => x.Tstamp).ToList()
            //       .Select(x => new LoadService()
            //       {
            //           Meter_Name = x.Meter_Name,
            //           METERID = x.METERID,
            //           Date = x.Tstamp.ToString(RptDispalyFormat),
            //           kWh_Exp = x.kWh_Exp,
            //           kWh_Imp = x.kWh_Imp,
            //           kVAh_Exp = x.kVAh_Exp,
            //           kVAh_Imp = x.kVAh_Imp,
            //           kVARh_Lag_Imp = x.kVARh_Lag_Imp,
            //           kVARh_Lead_Imp = x.kVARh_Lead_Imp,
            //           kVARh_Lag_Exp = x.kVARh_Lag_Exp,
            //           KVARh_Lead_exp = x.KVARh_Lead_exp
            //       }).ToList();


            //}

            return GetFilterData(Convert.ToString("B"), loadService);
        }

        public List<LoadService> GetMeterviewconsumptionExportImport(clsMeterviewModel model, string condition)
        {
            List<LoadService> loadService = new List<LoadService>();

            //DateTime fromDate = new DateTime();
            //DateTime toDate = new DateTime();
            //if (model.StartTime == null || model.StartTime == "")
            //    model.StartTime = "12:00 AM";
            //if (model.EndTime == null || model.EndTime == "")
            //    model.EndTime = "11:45 PM";

            //if (model.FromDate != null)
            //{
            //    fromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //    fromDate = Convert.ToDateTime(fromDate).AddMinutes(-15);
            //}
            //if (model.ToDate != null)
            //{
            //    if (model.EndTime != "11:45 PM")
            //    {
            //        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //        toDate = Convert.ToDateTime(toDate).AddMinutes(15);
            //    }
            //    else
            //    {
            //        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            //        toDate = Convert.ToDateTime(toDate).AddMinutes(30);
            //    }
            //}
            //if (model.Param.ToUpper() == "CI")
            //{

            //    loadService = (from m in db.evt_conimp_blockwise
            //                   join mtr in db.meters on m.meterid equals mtr.id
            //                   where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.ts > fromDate && m.ts < toDate
            //                   select new
            //                   {
            //                       Meter_Name = mtr.metername,
            //                       METERID = mtr.id,
            //                       Tstamp = (DateTime)m.ts,
            //                       kWh_Imp = m.cum_kwh,
            //                       kVAh_Imp = m.cum_kvah,
            //                       kVARh_Lag_Imp = m.cum_kvarh_lag,
            //                       kVARh_Lead_Imp = m.cum_kvarh_lead
            //                   }).OrderBy(x => x.Tstamp).ToList()
            //                   .Select(x => new LoadService()
            //                   {
            //                       Meter_Name = x.Meter_Name,
            //                       METERID = x.METERID,
            //                       Date = x.Tstamp.ToString(RptDispalyFormat),
            //                       kWh_Imp = x.kWh_Imp,
            //                       kVAh_Imp = x.kVAh_Imp,
            //                       kVARh_Lag_Imp = x.kVARh_Lag_Imp,
            //                       kVARh_Lead_Imp = x.kVARh_Lead_Imp
            //                   }).ToList();

            //    return GetFilterData(Convert.ToString("B"), loadService);
            //}
            //else if (model.Param.ToUpper() == "CE")
            //{


            //    loadService = (from m in db.evt_conexp_blockwise
            //                   join mtr in db.meters on m.meterid equals mtr.id
            //                   where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.ts > fromDate && m.ts < toDate
            //                   select new
            //                   {
            //                       Meter_Name = mtr.metername,
            //                       METERID = mtr.id,
            //                       Tstamp = (DateTime)m.ts,
            //                       kWh_Exp = m.cum_kwh,
            //                       kVAh_Exp = m.cum_kvah,
            //                       kVARh_Lag_Exp = m.cum_kvarh_lag,
            //                       KVARh_Lead_exp = m.cum_kvarh_lead
            //                   }).OrderBy(x => x.Tstamp).ToList()
            //           .Select(x => new LoadService()
            //           {
            //               Meter_Name = x.Meter_Name,
            //               METERID = x.METERID,
            //               Date = x.Tstamp.ToString(RptDispalyFormat),
            //               kWh_Exp = x.kWh_Exp,
            //               kVAh_Exp = x.kVAh_Exp,
            //               kVARh_Lag_Exp = x.kVARh_Lag_Exp,
            //               KVARh_Lead_exp = x.KVARh_Lead_exp
            //           }).ToList();

            //    return GetFilterData(Convert.ToString("B"), loadService);
            //}
            return loadService;
        }

        public List<LoadService> GetFilterData(string Interval, List<LoadService> objList)
        {

            if (Interval.ToUpper() == "B")
            {
                return objList;
            }
            else if (Interval.ToUpper() == "HL")
            {
                objList = objList.Where(x => (Convert.ToInt16(x.BlockNo) / 2) == 0).ToList();
                return objList;
            }
            else if (Interval.ToUpper() == "H")
            {
                objList = objList.Where(x => (Convert.ToInt16(x.BlockNo) / 4) == 0).ToList();
                return objList;
            }
            else if (Interval.ToUpper() == "D")
            {
                objList = objList.Where(x => (Convert.ToInt16(x.BlockNo) / 96) == 0).ToList();
                return objList;
            }
            return objList;
        }

        public List<LoadService> GetProfileLog(ProfileLogViewModel model)
        {
            List<LoadService> loadService = new List<LoadService>();


            loadService = (from m in db.loadsurveylogs
                           join mtr in db.meters on m.meterid equals mtr.id
                           where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > model.fltrFromDate && m.tstamp < model.fltrToDate
                           select new
                           {
                               Meter_Name = mtr.metername,
                               METERID = mtr.id,
                               Tstamp = m.tstamp,
                               vll = m.vll,
                               i = m.i,
                               pf = m.pf,
                               kw = m.kw,
                               kvar = m.kvar,
                               kva = m.kva,
                               hz = m.hz,
                               vry = m.vry,
                               vyb = m.vyb,
                               vbr = m.vbr,
                               vrn = m.vrn,
                               vyn = m.vyn,
                               vbn = m.vbn,
                               vln = m.vln,
                               ir = m.ir,
                               iy = m.iy,
                               ib = m.ib,
                               kwr = m.kwr,
                               kwy = m.kwy,
                               kwb = m.kwb,
                               kva_r = m.kva_r,
                               kvay = m.kvay,
                               kvab = m.kvab,
                               kw_demand = m.kw_demand,
                               kva_demand = m.kva_demand,
                               BlockNo = m.blockno
                           }).OrderBy(x => x.Tstamp).ToList()
                            .Select(x => new LoadService()
                            {
                                Meter_Name = x.Meter_Name,
                                METERID = x.METERID,
                                Date = x.Tstamp.Value.ToString(RptDispalyFormat),
                                vll = x.vll == null ? x.vll : Math.Round(Convert.ToDouble(x.vll), 2),
                                i = x.i == null ? x.i : Math.Round(Convert.ToDouble(x.i), 2),
                                pf = x.pf == null ? x.pf : Math.Round(Convert.ToDouble(x.pf), 3),

                                kw = x.kw == null ? x.kw : Math.Round(Convert.ToDouble(x.kw), 2),

                                kvar = x.kvar == null ? x.kvar : Math.Round(Convert.ToDouble(x.kvar), 2),

                                kva = x.kva == null ? x.kva : Math.Round(Convert.ToDouble(x.kva), 2),

                                hz = x.hz == null ? x.hz : Math.Round(Convert.ToDouble(x.hz), 2),

                                vry = x.vry == null ? x.vry : Math.Round(Convert.ToDouble(x.vry), 2),

                                vyb = x.vyb == null ? x.vyb : Math.Round(Convert.ToDouble(x.vyb), 2),

                                vbr = x.vbr == null ? x.vbr : Math.Round(Convert.ToDouble(x.vbr), 2),

                                vrn = x.vrn == null ? x.vrn : Math.Round(Convert.ToDouble(x.vrn), 2),

                                vyn = x.vyn == null ? x.vyn : Math.Round(Convert.ToDouble(x.vyn), 2),

                                vbn = x.vbn == null ? x.vbn : Math.Round(Convert.ToDouble(x.vbn), 2),

                                vln = x.vln == null ? x.vln : Math.Round(Convert.ToDouble(x.vln), 2),

                                ir = x.ir == null ? x.ir : Math.Round(Convert.ToDouble(x.ir), 2),

                                iy = x.iy == null ? x.iy : Math.Round(Convert.ToDouble(x.iy), 2),

                                ib = x.ib == null ? x.ib : Math.Round(Convert.ToDouble(x.ib), 2),

                                kwr = x.kwr == null ? x.kwr : Math.Round(Convert.ToDouble(x.kwr), 2),

                                kwy = x.kwy == null ? x.kwy : Math.Round(Convert.ToDouble(x.kwy), 2),

                                kwb = x.kwb == null ? x.kwb : Math.Round(Convert.ToDouble(x.kwb), 2),

                                kva_r = x.kva_r == null ? x.kva_r : Math.Round(Convert.ToDouble(x.kva_r), 2),

                                kvay = x.kvay == null ? x.kvay : Math.Round(Convert.ToDouble(x.kvay), 2),

                                kvab = x.kvab == null ? x.kvab : Math.Round(Convert.ToDouble(x.kvab), 2),

                                kw_demand = x.kw_demand == null ? x.kw_demand : Math.Round(Convert.ToDouble(x.kw_demand), 2),

                                kva_demand = x.kva_demand == null ? x.kva_demand : Math.Round(Convert.ToDouble(x.kva_demand), 2),
                                BlockNo = x.BlockNo.ToString()


                            }).ToList();


            if (model.Interval.ToUpper() == "H")
            {
                loadService = loadService.Where(x => Convert.ToInt32(x.BlockNo) % 4 == 0).ToList();
            }
            else if (model.Interval.ToUpper() == "D")
            {
                loadService = loadService.Where(x => Convert.ToInt32(x.BlockNo) % 96 == 0).ToList();
            }
            //loadService = model.FromDate != null && model.ToDate != null ? db.loadsurveylogs.
            //       Where(m => m.meterid == model.MeterId && m.tstamp >= fromDate
            //           && m.tstamp <= toDate).ToList() :
            //       db.loadsurveylogs.Where(m => m.meterid == model.MeterId).ToList();

            return loadService.ToList();
        }

        public List<LoadService> GetEnergyLog(EnergyLogViewModel model)
        {
            List<LoadService> loadService = new List<LoadService>();



            model.Interval = model.Interval.ToUpper();


            loadService = (from m in db.loadsurveylogs
                           join mtr in db.meters on m.meterid equals mtr.id
                           where m.meterid == model.MeterId && (mtr.isdeleted == 0 || mtr.isdeleted == null) && m.tstamp > model.fltrFromDate && m.tstamp < model.fltrToDate
                           //   && ((model.Interval == model.Interval && (Convert.ToInt32(m.blockno) % 4) == 0) || (model.Interval  == model.Interval))
                           select new
                           {
                               Meter_Name = mtr.metername,
                               METERID = mtr.id,
                               Tstamp = (DateTime)m.tstamp,
                               kWh_Exp = m.kwh_exp,
                               kWh_Imp = m.kwh_imp,
                               kVAh_Exp = m.kvah_exp,
                               kVAh_Imp = m.kvah_imp,
                               kVARh_Lag_Imp = m.kvarh_lag_imp,
                               kVARh_Lead_Imp = m.kvarh_lead_imp,
                               kVARh_Lag_Exp = m.kvarh_lag_exp,
                               KVARh_Lead_exp = m.kvarh_lead_exp,
                               kvarh_exp_103 = m.kvarh_exp_103,
                               kvarh_exp_97 = m.kvarh_exp_97,
                               kvarh_imp_103 = m.kvarh_imp_103,
                               kvarh_imp_97 = m.kvarh_imp_97,
                               day_kwh_exp = m.day_kwh_exp,
                               day_kwh_imp = m.day_kwh_imp,
                               cblk_kwh_exp = m.cblk_kwh_exp,
                               cblk_kwh_imp = m.cblk_kwh_imp,
                               avg_hz = m.avg_hz,
                               avg_mw = m.avg_mw,
                               blockno = m.blockno

                           }).OrderBy(x => x.Tstamp).ToList()
                           .Select(x => new LoadService()
                           {
                               Meter_Name = x.Meter_Name,
                               METERID = x.METERID,
                               Date = x.Tstamp.ToString(RptDispalyFormat),
                               kWh_Exp = x.kWh_Exp,
                               kWh_Imp = x.kWh_Imp,
                               kVAh_Exp = x.kVAh_Exp,
                               kVAh_Imp = x.kVAh_Imp,
                               kVARh_Lag_Imp = x.kVARh_Lag_Imp,
                               kVARh_Lead_Imp = x.kVARh_Lead_Imp,
                               kVARh_Lag_Exp = x.kVARh_Lag_Exp,
                               KVARh_Lead_exp = x.KVARh_Lead_exp,
                               kvarh_exp_103 = x.kvarh_exp_103,
                               kvarh_exp_97 = x.kvarh_exp_97,
                               kvarh_imp_103 = x.kvarh_imp_103,
                               kvarh_imp_97 = x.kvarh_imp_97,
                               day_kwh_exp = x.day_kwh_exp,
                               day_kwh_imp = x.day_kwh_imp,
                               cblk_kwh_exp = x.cblk_kwh_exp,
                               cblk_kwh_imp = x.cblk_kwh_imp,
                               avg_hz = x.avg_hz,
                               avg_mw = x.avg_mw,
                               BlockNo = x.blockno.ToString()

                           }).ToList();



            if (model.Interval == "H")
            {
                loadService = loadService.Where(x => Convert.ToInt32(x.BlockNo) % 4 == 0).ToList();
            }
            else if (model.Interval == "D")
            {
                loadService = loadService.Where(x => Convert.ToInt32(x.BlockNo) % 96 == 0).ToList();
            }




            return loadService.ToList();
        }


        public async Task<DataTable> GetCommonData(CommonView model, string reportDisplayDate, string DateFormat)
        {
            //List<LoadService> loadService = new List<LoadService>();
            DataTable dt = new DataTable();
            var meters = string.Empty;
            foreach (var meter in model.Meters)
            {
                meters = meters + "'" + meter + "',";
            }
            int index = meters.LastIndexOf(',');
            meters = meters.Substring(0, index);

            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            if (model.StartTime == null || model.StartTime == "")
                model.StartTime = "12:00 AM";
            if (model.EndTime == null || model.EndTime == "")
                model.EndTime = "11:45 PM";

            if (model.FromDate != null)
            {
                fromDate = DateTime.ParseExact(model.FromDate + " " + model.StartTime, DateFormat, CultureInfo.InvariantCulture);
                fromDate = Convert.ToDateTime(fromDate).AddMinutes(-15);
            }
            if (model.ToDate != null)
            {
                if (model.ToDate != null)
                {
                    if (model.EndTime != "11:45 PM")
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, DateFormat, CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(15);
                    }
                    else
                    {
                        toDate = DateTime.ParseExact(model.ToDate + " " + model.EndTime, DateFormat, CultureInfo.InvariantCulture);
                        toDate = Convert.ToDateTime(toDate).AddMinutes(30);
                    }
                }
            }

            var query = string.Empty;

            string tbl = "loadsurveylogs";
            string[] checkTableList = ConfigurationManager.AppSettings["blockQuery"] == null ? tbl.Split(',') : ConfigurationManager.AppSettings["blockQuery"].ToString().Split(',');
            var filterByBlocks = string.Empty;
            if (model.Params.Count > 0)
            {
                var subQuery = string.Empty;
                var cols = "";
                int i = 1;
                Hashtable ht = new Hashtable();


                foreach (var param in model.Params)
                {
                    var str = param.Split('-');
                    if (!(ht.ContainsKey(str[1].ToString())))
                    {
                        ht.Add(str[1].ToString(), i);
                        if (checkTableList.Contains(str[1].ToString()))
                        {

                            if (model.Interval == "H")
                            {
                                filterByBlocks += " and mod(_" + i.ToString() + ".blockno,4)=0";
                            }
                            else if (model.Interval == "D")
                            {
                                filterByBlocks += " and mod(_" + i.ToString() + ".blockno,96)=0";
                            }

                        }

                        if (string.IsNullOrEmpty(subQuery))
                        {
                            subQuery += " from " + str[1].ToString() + " _" + i.ToString();
                        }
                        else
                        {
                            if (model.Params.Count > 1)
                            {
                                subQuery += " join " + str[1].ToString() + " _" + i.ToString() + " on _1.tstamp = _" + i.ToString() + ".tstamp and _1.meterid = _" + i.ToString() + ".meterid ";

                            }
                        }
                        i++;
                    }

                    cols = cols + "_" + ht[str[1]].ToString() + "." + str[0];

                    if (str.Length > 2)
                    {
                        cols += " " + str[2].ToString();
                    }

                    cols += " , ";




                }
                index = cols.LastIndexOf(',');
                cols = cols.Substring(0, index);


                query = " select TO_CHAR(_1.tstamp::datetime year to second,'" + reportDisplayDate + "') as tstamp "
                    + " ,(select metername from meters where id=_1.meterid) as Metername "
                    + " ," + cols + " " + subQuery + " where "
                    + " _1.tstamp>'" + fromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and _1.tstamp<'" + toDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and "
                    + " _1.meterid in (" + meters + ") " + filterByBlocks;


            }




            dt = await DatabaseHandler.Handler(query);
            return dt;
        }
    }
}
