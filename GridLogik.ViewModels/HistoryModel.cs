using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class HistoryModel
    {
        [Display(Name = "Query")]
        [Required(ErrorMessage = "Query name is required")]
        public int Queryid { get; set; }
        public string QueryName { get; set; }

        public string MeterGroupName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<MeterGroup> Groups { get; set; }
        public List<MeterModel> Meters { get; set; }
        // public DateTime FromDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        //public DateTime ToDate { get; set; }
        public List<string> StartTimeList { get; set; }
        public List<string> EndTimeList { get; set; }
        public List<InstanceDataAverageLog> InstanceDataAverageLogList { get; set; }
        public List<PropertyInfo> InstanceDataAverageLogColumn { get; set; }
        public List<prmglobal> QueryList { get; set; }

        [Display(Name = "Meter Group")]
        [Required(ErrorMessage = "MeterGroup is required")]
        public string MeterGroupId { get; set; }
        public List<MeterGroupDetail> MeterGroup { get; set; }

        // public List<QueryDetail> QueryList { get; set; }

        
        public string Csv { get; set; }
        public DateTime fltrFromDate { get; set; }
        public DateTime fltrToDate { get; set; }
        public string Interval { get; set; }
    }
}
