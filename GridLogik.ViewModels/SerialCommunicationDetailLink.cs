using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class SerialCommunicationDetailLink
    {
        public long id { get; set; }
        [Required]
        [Display(Name = "Ip Address")]
        public Nullable<long> converterid { get; set; }
        public Nullable<long> meteridd { get; set; }
        public Nullable<long> meterid { get; set; }
        [Display(Name = "Modbus Id")]
        public Nullable<long> modbusid { get; set; }
        public Nullable<short> active { get; set; }
        [Display(Name = "Selected Meters")]
        [Required(ErrorMessage = "Please Select Meter")]
        public virtual string[] multiplemeterID { get; set; }
        [Display(Name = "Ip Address")]
        public string ipaddress { get; set; }
        [Display(Name = "Meter Name")]
        public string metername { get; set; }
        public virtual string[] SelectedMeterID { get; set; }
        public virtual int[] ModbusIds { get; set; }
        public virtual string MeterNameDisplay { get; set; }
        public virtual Meter meter { get; set; }
        public virtual List<SerialCommunicationDetailLink> commDetailsList { get; set; }
        [Required]
        [Display(Name = "Communication Type")]
        public virtual long commtype { get; set; }
        public virtual string commtypename { get; set; }
        [Required]
        [Display(Name = "Comport")]
        public virtual string port { get; set; }
    }
}
