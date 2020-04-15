using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class clsHistoryDataExportAPI
    {
        public int Queryid { get; set; }
        public string QueryName { get; set; }
        public List<prmglobal> QueryList { get; set; }
        public long meterid { get; set; }
        public string blockno { get; set; }
        public string date { get; set; }
        public DateTime? tstamp { get; set; }
        public double parameter { get; set; }
        public string metername { get; set; }

        public List<MeterGroup> MeterGroup { get; set; }

        [Display(Name = "Meter Group")]
        public string MeterGroupId { get; set; }
    }
}
