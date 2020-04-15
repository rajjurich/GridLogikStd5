using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterGroupViewmodel
    {
        public long id { get; set; }
        public string groupname { get; set; }
        public int meterid { get; set; }
        public long groupid { get; set; }
        public string status { get; set; }
        public Nullable<short> isdeleted { get; set; }

        public string fltrFromDate { get; set; }
        public string fltrToDate { get; set; }
    }
}
