using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class DCSGFuelModel
    {
        public long id { get; set; }
        public Nullable<long> blockno { get; set; }
        public Nullable<decimal> dcvalue { get; set; }
        public Nullable<decimal> sgvalue { get; set; }
        public Nullable<int> revision { get; set; }
        public Nullable<long> timestampid { get; set; }
        public Nullable<decimal> fuelcost { get; set; }
        public Nullable<short> isdeleted { get; set; }
        public Nullable<System.DateTime> tstamp { get; set; }
        public Nullable<long> stageid { get; set; }

        public Nullable<decimal> SGPPA { get; set; }
        public Nullable<decimal> SCED { get; set; }
        public Nullable<decimal> RRAS { get; set; }
        public Nullable<decimal> AGC { get; set; }
    }
}
