using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class InstanceDataLog
    {
        //public long ID { get; set; }
        public long METERID { get; set; }
        public string Meter_Name { get; set; }
        //public Nullable<long> TimeStampID { get; set; }
        public string Date { get; set; }
        public Nullable<double> Vrn { get; set; }

        public Nullable<double> Vyn { get; set; }
        public Nullable<double> Vbn { get; set; }

        public Nullable<double> Vln { get; set; }
        public Nullable<double> Vry { get; set; }
        public Nullable<double> Vyb { get; set; }
        public Nullable<double> Vbr { get; set; }
        public Nullable<double> Vll { get; set; }
        public Nullable<double> Ir { get; set; }
        public Nullable<double> Iy { get; set; }
        public Nullable<double> Ib { get; set; }
        public Nullable<double> I { get; set; }
        public Nullable<double> PF { get; set; }
        public Nullable<double> kW { get; set; }
        public Nullable<double> kVAR { get; set; }
        public Nullable<double> kVA { get; set; }
        public Nullable<double> HZ { get; set; }
        public Nullable<double> PFr { get; set; }
        public Nullable<double> PFy { get; set; }
        public Nullable<double> PFb { get; set; }
        public Nullable<double> kWr { get; set; }
        public Nullable<double> kWy { get; set; }
        public Nullable<double> kWb { get; set; }
        public Nullable<double> kVARr { get; set; }
        public Nullable<double> kVARy { get; set; }
        public Nullable<double> kVARb { get; set; }
        public Nullable<double> kVA_r { get; set; }
        public Nullable<double> kVAy { get; set; }
        public Nullable<double> kVAb { get; set; }
        public Nullable<double> kW_Demand { get; set; }
        public Nullable<double> kVAR_Demand { get; set; }
        public Nullable<double> kVA_Demand { get; set; }
        public Nullable<double> kW_Max_Demand { get; set; }
        public Nullable<double> kVAR_Max_Demand { get; set; }
        public Nullable<double> kVA_Max_Demand { get; set; }
        public Nullable<double> kWh_Exp { get; set; }
        public Nullable<double> kWh_Imp { get; set; }
        public Nullable<double> kVAh_Exp { get; set; }
        public Nullable<double> kVAh_Imp { get; set; }
        public Nullable<double> kVARh_Lag_Imp { get; set; }
        public Nullable<double> kVARh_Lead_Imp { get; set; }
        public Nullable<double> kVARh_Lag_Exp { get; set; }
        public Nullable<double> KVARh_Lead_exp { get; set; }
        public Nullable<double> KVARh_Exp_103 { get; set; }
        public Nullable<double> KVARh_Exp_97 { get; set; }
        public Nullable<double> KVARh_Imp_103 { get; set; }
        public Nullable<double> KVARh_Imp_97 { get; set; }
        public Nullable<double> Day_KWh_Exp { get; set; }
        public Nullable<double> Day_KWh_Imp { get; set; }
        public Nullable<double> CBlk_KWh_Exp { get; set; }
        public Nullable<double> CBlk_KWh_Imp { get; set; }
        public Nullable<double> CBlk_kVARh_Lag_Imp { get; set; }
        public Nullable<double> CBlk_kVARh_Lead_Imp { get; set; }
        public Nullable<double> CBlk_kVARh_Lag_Exp { get; set; }
        public Nullable<double> CBlk_kVARh_Lead_exp { get; set; }
        public Nullable<double> CBlk_Avg_MW { get; set; }
        public Nullable<double> CBlk_Avg_Hz { get; set; }
        public Nullable<double> LBlk_TimestampID { get; set; }
        public Nullable<double> LBlk_Avg_MW { get; set; }
        public Nullable<double> Lblk_Avg_Hz { get; set; }
        public Nullable<double> LBlk_kWh_Exp { get; set; }
        public Nullable<double> LBlk_kWh_Imp { get; set; }
        public Nullable<double> LBlk_kVAh_Exp { get; set; }
        public Nullable<double> LBlk_kVAh_Imp { get; set; }
        public Nullable<double> LBlk_kVARh_Lag_Imp { get; set; }
        public Nullable<double> LBlk_kVARh_Lead_Imp { get; set; }
        public Nullable<double> LBlk_kVARh_Lag_Exp { get; set; }
        public Nullable<double> LBlk_KVARh_Lead_exp { get; set; }
        public Nullable<double> LBlk_KVARh_Exp_103 { get; set; }
        public Nullable<double> LBlk_KVARh_Exp_97 { get; set; }
        public Nullable<double> LBlk_KVARh_Imp_103 { get; set; }
        public Nullable<double> LBlk_KVARh_Imp_97 { get; set; }
        public Nullable<double> POTime { get; set; }
        public Nullable<double> LOTime { get; set; }
        public Nullable<double> KWh_Net { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
