using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class GroupDisplayMeterDataViewModel
    {
        public long? MeterId { get; set; }
        public string Time { get; set; }
        public long? GroupId { get; set; }
        public string MeterName { get; set; }
        public InstanceData instanceData { get; set; }
    }
}
