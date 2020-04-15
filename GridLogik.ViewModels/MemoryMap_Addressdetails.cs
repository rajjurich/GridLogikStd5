using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MemoryMap_Addressdetails
    {
        public long ID { get; set; }

        public long ModelID { get; set; }
        public long RangeId { get; set; }
        public string Address { get; set; }

        public string ParameterName { get; set; }

        public string DataType { get; set; }
        public string InstanceDataMapping { get; set; }
        public Nullable<double> MultiplicationFactor { get; set; }
        public string ByteCount { get; set; }

        public virtual MemoryMap_Range MemoryMap_Range { get; set; }
        public virtual MeterModel MeterModel { get; set; }

        public List<MeterModel> ListMeterModel { get; set; }
        public virtual InstanceData InstanceData { get; set; }
        public List<InstanceData> ListInstanceData { get; set; }
        public int Counter { get; set; }
        public List<MemoryMap_Addressdetails> ListAddressDetails1 { get; set; }
        public List<MemoryMap_Addressdetails> ListAddressDetails2 { get; set; }


        public int Day { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public int Hr { get; set; }

        public int Min { get; set; }

        public int Sec { get; set; }
    }
}
