using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class clsDailyReport
    {
        public double? kwh_imp { get; set; }
        public double? kwh_exp { get; set; }
        public long meterid { get; set; }
        public string metername { get; set; }
        public string blockno { get; set; }
        public DateTime? tstamp { get; set; }
    }
}
