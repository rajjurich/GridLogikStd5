using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class RealTimeTrend
    {
        [Required(ErrorMessage = "Please select Meter Group")]
        [Display(Name = "Meter Group")]
        public string MeterGroup { get; set; }

        [Required(ErrorMessage = "Please select Meter")]
        [Display(Name = "Meters")]
        public string Meters { get; set; }
    }
}
