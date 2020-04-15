using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Model
{
    public class MeterList
    {
        public string MeterString { get; set; }
        public string FromSelectionFilter { get; set; }
        public string ToSelectionFilter { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string CountParameter { get; set; }
        public string SumParameter { get; set; }
        public string AvgParameter { get; set; }
        public string MaxParameter { get; set; }
        public string MinParameter { get; set; }
        public string GroupParameter { get; set; }
        public string ColumnParameter { get; set; }
        public string GroupByColumnParameter { get; set; }
        public string OrderByColumnParameter { get; set; }
    }

    public class OpenQuery
    {
        public string Query { get; set; }
    }
}