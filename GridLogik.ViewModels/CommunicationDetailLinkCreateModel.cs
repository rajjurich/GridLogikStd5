using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class CommunicationDetailLinkCreateModel
    {
        public long id { get; set; }
        [Required]
        [Display(Name = "Communication Device")]
        public Nullable<long> converterid { get; set; }
        public List<MeterWithModBusId> meters { get; set; }
    }

    public class MeterWithModBusId
    {
        [Display(Name = "Meter Id")]
        [Required(ErrorMessage = "Please Enter Meter Id")]
        public long meterId { get; set; }
        public string meterName { get; set; }

        [Display(Name = "Modbus ID")]
        [Required(ErrorMessage = "Please Enter Modbus ID")]        
        public Nullable<long> modbusid { get; set; }
    }
}
