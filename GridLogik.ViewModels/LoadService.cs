using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class LoadService
    {
        public long ID { get; set; }
        public long METERID { get; set; }
        public string BlockNo { get; set; }
        public string BlockTime { get; set; }
        public string Meter_Name { get; set; }
        public string Date { get; set; }
        public Nullable<long> TimeStampID { get; set; }
        public Nullable<System.DateTime> Tstamp { get; set; }
        public Nullable<double> vll { get; set; }
        public Nullable<double> i { get; set; }
        public Nullable<double> pf { get; set; }
        public Nullable<double> kw { get; set; }
        public Nullable<double> kvar { get; set; }
        public Nullable<double> kva { get; set; }
        public Nullable<double> hz { get; set; }
        public Nullable<double> vry { get; set; }
        public Nullable<double> vyb { get; set; }
        public Nullable<double> vbr { get; set; }
        public Nullable<double> vrn { get; set; }
        public Nullable<double> vyn { get; set; }
        public Nullable<double> vbn { get; set; }
        public Nullable<double> vln { get; set; }
        public Nullable<double> ir { get; set; }
        public Nullable<double> iy { get; set; }
        public Nullable<double> ib { get; set; }
        public Nullable<double> kwr { get; set; }
        public Nullable<double> kwy { get; set; }
        public Nullable<double> kwb { get; set; }
        public Nullable<double> kva_r { get; set; }
        public Nullable<double> kvay { get; set; }
        public Nullable<double> kvab { get; set; }
        public Nullable<double> kw_demand { get; set; }
        public Nullable<double> kva_demand { get; set; }
        public Nullable<double> kw_max_demand { get; set; }
        public Nullable<double> kva_max_demand { get; set; }
        public Nullable<double> kWh_Exp { get; set; }
        public Nullable<double> kWh_Imp { get; set; }
        public Nullable<double> kVAh_Exp { get; set; }
        public Nullable<double> kVAh_Imp { get; set; }
        public Nullable<double> kVARh_Lag_Imp { get; set; }
        public Nullable<double> kVARh_Lead_Imp { get; set; }
        public Nullable<double> kVARh_Lag_Exp { get; set; }
        public Nullable<double> KVARh_Lead_exp { get; set; }
        public Nullable<double> kvarh_exp_103 { get; set; }
        public Nullable<double> kvarh_exp_97 { get; set; }
        public Nullable<double> kvarh_imp_103 { get; set; }
        public Nullable<double> kvarh_imp_97 { get; set; }
        public Nullable<double> day_kwh_exp { get; set; }
        public Nullable<double> day_kwh_imp { get; set; }
        public Nullable<double> cblk_kwh_exp { get; set; }
        public Nullable<double> cblk_kwh_imp { get; set; }
        public Nullable<double> avg_hz { get; set; }
        public Nullable<double> avg_mw { get; set; }
        public Nullable<double> runhr { get; set; }
        public virtual Meter Meter { get; set; }
    }
}
