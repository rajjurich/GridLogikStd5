using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterStatus
    {

        public long ID { get; set; }

        [Display(Name = "ID")]
        public long METERID { get; set; }

        [Display(Name = "Meter Name")]
        public string Meter_Name { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime? Date_Time { get; set; }


        [Display(Name = "IP Adress")]
        public string IpAddress { get; set; }

        [Display(Name = "Meter Type")]
        public string MeterType1 { get; set; }

        [Display(Name = "Meter Model")]
        public string ModelName { get; set; }

        [Display(Name = "Utility")]
        public string UtilityName { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Mod Bus Id")]
        public long ModBusID { get; set; }
        [Display(Name = "Serial No")]
        public string SerialNo { get; set; }
        [Display(Name = "Port")]
        public string Port { get; set; }
    }
}
