using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstTOU
    {
        public long tourecid { get; set; }

        [Display(Name = "TOD Code")]
        //[CustRequiredAttribute("touid")]
        //[Pad]
        [Required(ErrorMessage = "Please enter TOD Code")]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage="Invalid TOD Code")]
        public string touid { get; set; }

        [Display(Name = "Consumer Category")]
        //[CustRequiredAttribute("touconsumercatid")]
        [Required(ErrorMessage = "Please select Consumer Category")]
        public string touconsumercatid { get; set; }

        [Display(Name = "Maximum Slots")]
        //[CustRequiredAttribute("toumaxslots")]
        [Required(ErrorMessage = "Please enter Max Slots")]
        [Range(1, 12, ErrorMessage = "Maximum 12 slots are allowed")]
        public Nullable<int> toumaxslots { get; set; }

        //[CustRequiredAttribute("toumaxslots")]
        [Required(ErrorMessage = "Please enter Description")]
        [Display(Name = "Description")]
        public string toudescription { get; set; }
        public Nullable<short> touisdeleted { get; set; }


        //

        //public string tsslotno { get; set; }
        //[Required(ErrorMessage = "Please enter")]
        //public string tsslotstart { get; set; }

        //[Required]
        //public string tsslotend { get; set; }
        //public string tsmaxdemandlimit { get; set; }
    }
}