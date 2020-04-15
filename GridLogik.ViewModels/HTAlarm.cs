using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class HTAlarm
    {
        public long ID { get; set; }
        public long? alarmid { get; set; }
        public string metername { get; set; }
        public string location { get; set; }
        public string onoff { get; set; }
        public string alarmname { get; set; }
        public string alarmmessage { get; set; }
        public string stoptimelog { get; set; }
        public string starttimelog { get; set; }
        public string converterip { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public Nullable<int> duration { get; set; }

        public string fltrFromDate { get; set; }
        public string fltrToDate { get; set; }


        public short? ismanualack { get; set; }
        public bool flagchecked
        {
            get { return ismanualack == 1; }
            set { ismanualack = value ? (short)1 : (short)0; }
        }
        public string AllChecked { get; set; }
        public bool offChecked { get; set; }

    }
}
