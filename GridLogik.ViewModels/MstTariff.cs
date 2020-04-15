using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MstTariff
    {
        public long trfrecid { get; set; }
        public string trfid { get; set; }
        public string trftouid { get; set; }
        public string trftouslotid { get; set; }
        public string trfschemename { get; set; }
        public Nullable<short> trfisdeleted { get; set; }
        public string trfutilityid { get; set; }

    }
}
