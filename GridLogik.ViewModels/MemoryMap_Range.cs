using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MemoryMap_Range
    {
        public long ID { get; set; }
        public long ModelID { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public string CountAddress { get; set; }
        public string RangeType { get; set; }

        public virtual ICollection<MemoryMap_Addressdetails> MemoryMap_Addressdetails { get; set; }
        public virtual MeterModel MeterModel { get; set; }
    }
}
