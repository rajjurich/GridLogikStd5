using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class CommunicationDetailLinkListModel
    {
        public long? ConvertorId { get; set; }
        public string IpAddress { get; set; }
        public string MeterNames { get; set; }
        public string Port { get; set; }
    }
}
