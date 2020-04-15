using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class CentralHzUI
    {
        public string UnitName { get; set; }
        public List<fetchData> PreBlk { get; set; }
        public List<fetchData> TodaysAvg { get; set; }
        public List<fetchData> PreDaysAvg { get; set; }
    }

    public class fetchData
    {
        public double? AvgHz { get; set; }
        public double? UiRate { get; set; }
    }
}
