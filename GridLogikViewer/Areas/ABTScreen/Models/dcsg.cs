using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Areas.ABTScreen.Models
{
    public class dcsg
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
    }
}