using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class HormonicDayThd
    {
        [Required(ErrorMessage = "Please select Meter Group")]
        [Display(Name = "Meter Group")]
        public string MeterGroup { get; set; }

        [Required(ErrorMessage = "Please select Meter")]
        [Display(Name = "Meter Name")]
        public string Meters { get; set; }

        public string Date { get; set; }
        [Display(Name = "Parameter")]
        [Required(ErrorMessage = "Please select parameter")]
        public string Parameter { get; set; }
    }
}
