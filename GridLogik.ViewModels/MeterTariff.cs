using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterTariff
    {
        public long id { get; set; }
        public string tarifid { get; set; }
        public Nullable<long> meterid { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public Nullable<short> isdeleted { get; set; }

    }
}
