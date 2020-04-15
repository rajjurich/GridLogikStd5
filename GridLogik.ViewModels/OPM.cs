using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class OPM
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> PortList { get; set; }
        public List<string> MeterList { get; set; }
        public List<string> TagList { get; set; }
    }
}
