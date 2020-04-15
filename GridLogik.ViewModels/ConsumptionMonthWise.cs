using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ConsumptionMonthWise
    {
        [Required(ErrorMessage = "Please select Meter Group")]
        [Display(Name = "Meter Group")]
        public string MeterGroup { get; set; }

        [Required(ErrorMessage = "Please select Meter")]
        [Display(Name = "Meter Name")]
        public string Meters { get; set; }

        [Display(Name = "Month")]
        [Required(ErrorMessage = "Please select Month")]
        public string Months { get; set; }
        [Display(Name = "Year")]
        [Required(ErrorMessage = "Please select Year")]
        public string Years { get; set; }
    }
}
