using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstErrorLog
    {
            public long errorid { get; set; }
            public string errordescription { get; set; }
            public string errortrace { get; set; }
            public string errormodule { get; set; }
            public Nullable<System.DateTime> errordate { get; set; }
        
    }
}