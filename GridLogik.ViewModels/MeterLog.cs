using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterLog
    {
        public long id { get; set; }
        public long meterid { get; set; }
        public string metername { get; set; }
        public string location { get; set; }
        public string stoptime { get; set; }
        public string starttime { get; set; }
        public string type { get; set; }
        public string converterip { get; set; }
        public string fltrFromDate { get; set; }
        public string fltrToDate { get; set; }
    }
}
