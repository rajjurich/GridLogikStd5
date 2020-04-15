using Domain.Entities;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ITrendsDataService
    {
        List<InstanceDataLog> GetTrendsData(TrendsDataViewModel model);
        List<InstanceDataLog> GetTrendsDataLog(TrendsDataViewModel model);
    }
    public class TrendsDataService : ITrendsDataService
    {
        etools_devEntities db;
        IPrmGlobalService prmGlobalService;
        static string RptDispalyFormat = string.Empty;
        public TrendsDataService(DbContext db, IPrmGlobalService prmGlobalService)
        {
            this.db = (etools_devEntities)db;
            this.prmGlobalService = prmGlobalService;
            RptDispalyFormat = prmGlobalService.FindBy(prm => (prm.prmmodule.ToUpper() == "GLOBAL" && prm.prmunit.ToUpper() == "REPORT_DISPLAY" && prm.prmidentifier.ToUpper() == "FORMAT")).Select(prm => prm.prmvalue).FirstOrDefault();
        }
        public List<InstanceDataLog> GetTrendsData(TrendsDataViewModel model)
        {
            // DALGlobalS objGlobal = new DALGlobalS();
            //  string RptDispalyFormat = objGlobal.GetReportDisplayDate();

            List<InstanceDataLog> instanceDataLogList = new List<InstanceDataLog>();
            instanceDataLogList = (from m in db.instancedatalogs
                                   where m.meterid == model.MeterId && m.tstamp > model.fltrFromDate && m.tstamp < model.fltrToDate
                                   select new
                                   {
                                       METERID = m.meterid,
                                       Meter_Name = m.meter_name,
                                       Date = m.tstamp,
                                       Vrn = m.vrn,
                                       Vbn = m.vbn,
                                       Vyn = m.vyn,
                                       Vln = m.vln,
                                       Vry = m.vry,
                                       Vyb = m.vyb,
                                       Vbr = m.vbr,
                                       Vll = m.vll,
                                       Ir = m.ir,
                                       Iy = m.iy,
                                       Ib = m.ib,
                                       I = m.i,
                                       PF = m.pf,
                                       kW = m.kw,
                                       kVAR = m.kvar,
                                       kVA = m.kva,
                                       HZ = m.hz

                                   }).OrderBy(a => a.Date).ToList()
                                   .Select(x => new InstanceDataLog()
                                   {
                                       METERID = x.METERID,
                                       Meter_Name = x.Meter_Name,
                                       Date = x.Date.Value.ToString(RptDispalyFormat),
                                       Vrn = x.Vrn == null ? x.Vrn : Math.Round(Convert.ToDouble(x.Vrn), 2),
                                       Vbn = x.Vbn == null ? x.Vbn : Math.Round(Convert.ToDouble(x.Vbn), 2),
                                       Vyn = x.Vyn == null ? x.Vyn : Math.Round(Convert.ToDouble(x.Vyn), 2),
                                       Vln = x.Vln == null ? x.Vln : Math.Round(Convert.ToDouble(x.Vln), 2),
                                       Vry = x.Vry == null ? x.Vry : Math.Round(Convert.ToDouble(x.Vry), 2),
                                       Vyb = x.Vyb == null ? x.Vyb : Math.Round(Convert.ToDouble(x.Vyb), 2),
                                       Vbr = x.Vbr == null ? x.Vbr : Math.Round(Convert.ToDouble(x.Vbr), 2),
                                       Vll = x.Vll == null ? x.Vll : Math.Round(Convert.ToDouble(x.Vll), 2),
                                       Ir = x.Ir == null ? x.Ir : Math.Round(Convert.ToDouble(x.Ir), 2),
                                       Iy = x.Iy == null ? x.Iy : Math.Round(Convert.ToDouble(x.Iy), 2),
                                       Ib = x.Ib == null ? x.Ib : Math.Round(Convert.ToDouble(x.Ib), 2),
                                       I = x.I == null ? x.I : Math.Round(Convert.ToDouble(x.I), 2),
                                       PF = x.PF == null ? x.PF : Math.Round(Convert.ToDouble(x.PF), 3),
                                       kW = x.kW == null ? x.kW : Math.Round(Convert.ToDouble(x.kW), 2),
                                       kVAR = x.kVAR == null ? x.kVAR : Math.Round(Convert.ToDouble(x.kVAR), 2),
                                       kVA = x.kVA == null ? x.kVA : Math.Round(Convert.ToDouble(x.kVA), 2),
                                       HZ = x.HZ == null ? x.HZ : Math.Round(Convert.ToDouble(x.HZ), 2),

                                   }).ToList();
            //    && m.tstamp < toDate)
            return instanceDataLogList;
            //return db.instancedatalogs.ToList();
        }

        public List<InstanceDataLog> GetTrendsDataLog(TrendsDataViewModel model)
        {
         

            List<InstanceDataLog> instanceDataLogList = new List<InstanceDataLog>();
            //instanceDataLogList = (from m in db.evt_30secdatalogs
            //                       where m.meterid == model.MeterId && m.ts > model.fltrFromDate && m.ts < model.fltrToDate
            //                       select new
            //                       {
            //                           METERID = m.meterid,
            //                           Date = m.ts,
            //                           Vrn = m.t_vrn,
            //                           Vbn = m.t_vbn,
            //                           Vyn = m.t_vyn,
            //                           Vln = m.t_vln,
            //                           Vry = m.t_vry,
            //                           Vyb = m.t_vyb,
            //                           Vbr = m.t_vbr,
            //                           Vll = m.t_vll,
            //                           Ir = m.t_ir,
            //                           Iy = m.t_iy,
            //                           Ib = m.t_ib,
            //                           I = m.t_i,
            //                           PF = m.t_pf,
            //                           kW = m.t_kw,
            //                           kVAR = m.t_kvar,
            //                           kVA = m.t_kva,
            //                           HZ = m.t_hz

            //                       }).ToList()
            //                       .Select(x => new InstanceDataLog()
            //                       {
            //                           METERID = x.METERID,
            //                           Date = x.Date.Value.ToString(RptDispalyFormat),
            //                           Vrn = x.Vrn == null ? x.Vrn : Math.Round(Convert.ToDouble(x.Vrn), 2),
            //                           Vbn = x.Vbn == null ? x.Vbn : Math.Round(Convert.ToDouble(x.Vbn), 2),
            //                           Vyn = x.Vyn == null ? x.Vyn : Math.Round(Convert.ToDouble(x.Vyn), 2),
            //                           Vln = x.Vln == null ? x.Vln : Math.Round(Convert.ToDouble(x.Vln), 2),
            //                           Vry = x.Vry == null ? x.Vry : Math.Round(Convert.ToDouble(x.Vry), 2),
            //                           Vyb = x.Vyb == null ? x.Vyb : Math.Round(Convert.ToDouble(x.Vyb), 2),
            //                           Vbr = x.Vbr == null ? x.Vbr : Math.Round(Convert.ToDouble(x.Vbr), 2),
            //                           Vll = x.Vll == null ? x.Vll : Math.Round(Convert.ToDouble(x.Vll), 2),
            //                           Ir = x.Ir == null ? x.Ir : Math.Round(Convert.ToDouble(x.Ir), 2),
            //                           Iy = x.Iy == null ? x.Iy : Math.Round(Convert.ToDouble(x.Iy), 2),
            //                           Ib = x.Ib == null ? x.Ib : Math.Round(Convert.ToDouble(x.Ib), 2),
            //                           I = x.I == null ? x.I : Math.Round(Convert.ToDouble(x.I), 2),
            //                           PF = x.PF == null ? x.PF : Math.Round(Convert.ToDouble(x.PF), 3),
            //                           kW = x.kW == null ? x.kW : Math.Round(Convert.ToDouble(x.kW), 2),
            //                           kVAR = x.kVAR == null ? x.kVAR : Math.Round(Convert.ToDouble(x.kVAR), 2),
            //                           kVA = x.kVA == null ? x.kVA : Math.Round(Convert.ToDouble(x.kVA), 2),
            //                           HZ = x.HZ == null ? x.HZ : Math.Round(Convert.ToDouble(x.HZ), 2),
            //                       }).ToList();

            return instanceDataLogList;
            
        }
    }
}
