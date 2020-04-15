using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Areas.ABTScreen.Models
{
    public class FuelRate
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please select Date")]
        [Display(Name = "Date")]
        public string app_from { get; set; }

        [Required(ErrorMessage = "Please select rate")]
        [Display(Name = "Rate")]
        public Nullable<double> Rate { get; set; }

        public short IsDeleted { get; set; }

        public DateTime? Date { get; set; }
    }
}