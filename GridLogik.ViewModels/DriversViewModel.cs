using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class DriversViewModel
    {
        public MeterModel MeterModel { get; set; }
        public List<MemoryMap_Range> Range { get; set; }
        public List<MemoryMap_Addressdetails> AddressDetails { get; set; }
    }
}
