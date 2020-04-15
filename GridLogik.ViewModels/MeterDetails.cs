using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterDetails
    {
        public long id { get; set; }
        public string metername { get; set; }
        public string metertype { get; set; }
        public string model { get; set; }
        public string communicationtype { get; set; }
        public string serialno { get; set; }
        public string meterno { get; set; }
        public Nullable<long> modbusid { get; set; }
        public Nullable<long> ctprimary { get; set; }
        public Nullable<long> ctsecondary { get; set; }
        public string location { get; set; }
        public List<ParameterMF> parametermf { get; set; }
        public Nullable<long> rolloverlimit { get; set; }
    }
}
