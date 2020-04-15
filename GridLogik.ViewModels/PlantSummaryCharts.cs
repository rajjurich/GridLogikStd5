using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class PlantSummaryCharts
    {
        public List<PlantSummaryChartHz> plantSummaryChartHzs { get; set; }
        public List<PlantSummaryChartAvgHz> plantSummaryChartAvgHzs { get; set; }
        public List<PlantSummaryChartDefaultHz> plantSummaryChartDefaultHzs { get; set; }
    }
    public class PlantSummaryChart
    {
        public PlantSummaryChartHz plantSummaryChartHz { get; set; }
        public PlantSummaryChartAvgHz plantSummaryChartAvgHz { get; set; }
        public PlantSummaryChartDefaultHz plantSummaryChartDefaultHz { get; set; }
    }
    public class PlantSummaryChartHz
    {
        public DateTime tstamp { get; set; }
        public decimal hz { get; set; }
    }
    public class PlantSummaryChartAvgHz
    {
        public DateTime tstamp { get; set; }
        public decimal avghz { get; set; }
    }
    public class PlantSummaryChartDefaultHz
    {
        public DateTime tstamp { get; set; }
        public decimal defaulthz { get; set; }
    }
}
