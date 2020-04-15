using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class HeatRate
    {
        public long mrecid { get; set; }
        public long mgenid { get; set; }
        public Nullable<double> mheatrate { get; set; }
        public Nullable<double> mgmw { get; set; }
    }
}
