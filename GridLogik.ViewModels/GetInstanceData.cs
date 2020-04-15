using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class GetInstanceData
    {
        #region Sagar Ranpise(TSO) TSO WAS ENOUGH
        public long id { get; set; }
        public long meterid { get; set; }
        public string meter_name { get; set; }
        public DateTime? tstamp { get; set; }
        public Nullable<double> kw { get; set; }

        public Nullable<double> Hz { get; set; }

        public Nullable<double> kwh_exp { get; set; }
        public Nullable<double> kwh_imp { get; set; }
        #endregion
    }
}
