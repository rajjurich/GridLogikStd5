using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterHistoryModel
    {
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "From Date is required")]
        public string FromDate { get; set; }
        [Display(Name = "To Date")]
        [Required(ErrorMessage = "To Date is required")]
        public string ToDate { get; set; }
    }
}
