using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class modlimit
    {
        public long mrecid { get; set; }
        public long mgenid { get; set; }
        public long mblockno { get; set; }
        public Nullable<long> moperation { get; set; }
        public Nullable<double> mminsch { get; set; }
        public Nullable<double> mmaxsch { get; set; }
    }
}