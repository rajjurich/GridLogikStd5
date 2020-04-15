using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstHoliday
    {
        [Display(Name = "Holiday RecID")]
        public long holrecid { get; set; }

        [CustRequiredAttribute("holid")]
        [Display(Name = "Holiday ID")]
        [Pad]
        public string holid { get; set; }

        [CustRequiredAttribute("holdescription")]
        [Display(Name = "Description")]
        public string holdescription { get; set; }

        [CustRequiredAttribute("holdate")]
        [Display(Name = "Holiday date")]
        public Nullable<System.DateTime> holdate { get; set; }

        [CustRequiredAttribute("hollegaldate")]
        [Display(Name = "Holiday legal date")]
        public Nullable<System.DateTime> hollegaldate { get; set; }
        
        [Display(Name = "Is deleted")]
        public Nullable<short> holisdeleted { get; set; }
    }
}