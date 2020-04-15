﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class clsMeterviewModel
    {
        public long MeterId { get; set; }
        public long GroupId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Param { get; set; }
        public string Interval { get; set; }
    }

    public class CommonViewModel
    {
        public List<long> Meters { get; set; }
        public long GroupId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> Params { get; set; }
        public string Interval { get; set; }
    }
}
