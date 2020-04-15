using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class TotalConsumption
    {

        [Required(ErrorMessage = "Please select Utility")]
        [Display(Name = "Utility")]
        public string Utility { get; set; }

        [Required(ErrorMessage = "Please select Meter Group")]
        [Display(Name = "Meter Group")]
        public string MeterGroup { get; set; }

        [Required(ErrorMessage = "Please select Meter")]
        [Display(Name = "Meters")]
        public string Meters { get; set; }
        [Required(ErrorMessage = "Please select From Date")]
        [Display(Name = "From Date")]
        public string FromDate { get; set; }


        [Required(ErrorMessage = "Please select To Date")]
        [Display(Name = "To Date")]
        public string ToDate { get; set; }
    }
}
