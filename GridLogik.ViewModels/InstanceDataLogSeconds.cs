using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class InstanceDataLogSeconds
    {
        public long ID { get; set; }
        public long METERID { get; set; }
        public string Meter_Name { get; set; }
        public Nullable<long> TimeStampID { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<double> t_Vrn { get; set; }
        public Nullable<double> t_Vbn { get; set; }
        public Nullable<double> t_Vyn { get; set; }
        public Nullable<double> t_Vln { get; set; }
        public Nullable<double> t_Vry { get; set; }
        public Nullable<double> t_Vyb { get; set; }
        public Nullable<double> t_Vbr { get; set; }
        public Nullable<double> t_Vll { get; set; }
        public Nullable<double> t_Ir { get; set; }
        public Nullable<double> t_Iy { get; set; }
        public Nullable<double> t_Ib { get; set; }
        public Nullable<double> t_I { get; set; }
        public Nullable<double> t_PF { get; set; }
        public Nullable<double> t_kW { get; set; }
        public Nullable<double> t_kVAR { get; set; }
        public Nullable<double> t_kVA { get; set; }
        public Nullable<double> t_HZ { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
