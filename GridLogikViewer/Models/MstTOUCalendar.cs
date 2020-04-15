using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Models
{
    public class MstTOUCalendar
    {
        public long tcrecid { get; set; }

        [Display(Name = " Code")]
        [CustRequiredAttribute("tctouid")]
        [Pad]
        public string tctouid { get; set; }

        [Display(Name = "Monday")]
        [CustRequiredAttribute("tcmonday")]
        public bool tcmonday { get; set; }

        [Display(Name = "Tuesday")]
        [CustRequiredAttribute("tctuesday")]
        public bool tctuesday { get; set; }

        [Display(Name = "Wednesday")]
        [CustRequiredAttribute("tcwednesday")]
        public bool tcwednesday { get; set; }

        [Display(Name = "Thursday")]
        [CustRequiredAttribute("tcthursday")]
        public bool tcthursday { get; set; }

        [Display(Name = "Friday")]
        [CustRequiredAttribute("tcfriday")]
        public bool tcfriday { get; set; }

        [Display(Name = "Saturday")]
        [CustRequiredAttribute("tcsaturday")]
        public bool tcsaturday { get; set; }

        [Display(Name = "Sunday")]
        [CustRequiredAttribute("tcsunday")]
        public bool tcsunday { get; set; }

        [Display(Name = "Holiday")]
        [CustRequiredAttribute("tcholiday")]
        public bool tcholiday { get; set; }
        public Nullable<bool> rolisdeleted { get; set; }
    }
}
