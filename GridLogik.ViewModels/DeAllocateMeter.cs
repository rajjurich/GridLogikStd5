using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class DeAllocateMeter
    {
        [Display(Name = "Consumer")]
        [Required(ErrorMessage = "Please Select Consumer")]
        public string drpConsumerID { get; set; }

        [Display(Name = " Meter")]
        [Required(ErrorMessage = "Please Select Meter")]
        public string drpMeterID { get; set; }
    }
}
