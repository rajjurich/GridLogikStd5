using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class uirate
    {
        public long id { get; set; }
        public Nullable<decimal> frequency { get; set; }
        public Nullable<decimal> positive { get; set; }
        public Nullable<decimal> negative { get; set; }
        public Nullable<int> stageid { get; set; }
    }
}