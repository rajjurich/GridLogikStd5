using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GridLogik.ViewModels
{
    public class EnergyLogViewModel
    {
        [Display(Name = "Meter Name")]
        [Required(ErrorMessage = "Meter name is required")]
        public int MeterId { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group name is required")]
        public int GroupId { get; set; }
        public string Meter_Name { get; set; }
        public string Group_Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<MeterGroup> Groups { get; set; }
        public List<MeterModel> Meters { get; set; }
        // [DisplayFormat(DataFormatString = "dd/mm/yy", ApplyFormatInEditMode = true)]
        public string FromDate { get; set; }
        //[DisplayFormat(DataFormatString = "dd/mm/yy", ApplyFormatInEditMode = true)]
        public string ToDate { get; set; }
        public List<string> StartTimeList { get; set; }
        public List<string> EndTimeList { get; set; }
        public List<LoadService> LoadServiceList { get; set; }
        public List<PropertyInfo> LoadServiceColumn { get; set; }

        [AllowHtml]
        public string Csv { get; set; }
        public DateTime fltrFromDate { get; set; }
        public DateTime fltrToDate { get; set; }
        public string ColumnValue { get; set; }
        public string SelectedColumnOrder { get; set; }
        public string Interval { get; set; }
    }
}
