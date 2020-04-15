using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class UiRateDatewise
    {
        public long id { get; set; }
        public Nullable<decimal> frequency { get; set; }
        public Nullable<decimal> positive { get; set; }
        public Nullable<decimal> negative { get; set; }
        public long stageid { get; set; }

        public System.DateTime tstamp { get; set; }
        public Nullable<int> revision { get; set; }
        public DateTime UiRateDate { get; set; }
    }
}
