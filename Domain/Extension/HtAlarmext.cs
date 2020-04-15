using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    public class HtAlarmext:htalarm
    {
        public string metername { get; set; }
        public string location { get; set; }

        public string stoptimelog { get; set; }
        public string starttimelog { get; set; }

        public string converterip { get; set; }

        public string fltrFromDate { get; set; }
        public string fltrToDate { get; set; }

        public string onoff { get; set; }
    }
}
