using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GridLogik.ViewModels
{
    public class GroupDisplayViewModel
    {
        public int? Queryid { get; set; }
        public string Query { get; set; }
        public string TableName { get; set; }
        public string Parameters { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool flag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> StartTimeList { get; set; }
        public List<string> EndTimeList { get; set; }

        public List<string> ParametrsList { get; set; }
        public int Groupid { get; set; }

        public string GroupName { get; set; }

        public List<MeterGroup> GroupNameList { get; set; }
        public List<GroupTemplateQuery> QueryList { get; set; }

        [AllowHtml]
        public string Csv { get; set; }
    }
}
