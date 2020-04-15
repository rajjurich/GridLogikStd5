using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ConsumptionHourWise
    {
        [Required(ErrorMessage = "Please select Meter Group")]
        [Display(Name = "Meter Group")]
        public string MeterGroup { get; set; }

        [Required(ErrorMessage = "Please select Meter")]
        [Display(Name = "Meter Name")]
        public string Meters { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please select Date")]
        public string CompareDate { get; set; }


        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please select Date")]
        public string WithDate { get; set; }
    }
}
