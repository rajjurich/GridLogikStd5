using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstFeeder
    {        
        [Display(Name = "Feeder Rec Id")]        
        public long fdrrecid { get; set; }

        [CustRequiredAttribute("fdrid")]
        [Display(Name = "Feeder Id")]
        [Pad]
        public string fdrid { get; set; }

        [CustRequiredAttribute("fdrname")]
        [Display(Name = "Name")]  
        public string fdrname { get; set; }

        [CustRequiredAttribute("fdrdescription")]
        [Display(Name = "Description")]  
        public string fdrdescription { get; set; }

        [CustRequiredAttribute("fdrserialno")]
        [Display(Name = "Serial no")]  
        public string fdrserialno { get; set; }

        [CustRequiredAttribute("fdrratedvoltage")]
        [Display(Name = "Rated Voltage")]  
        public Nullable<decimal> fdrratedvoltage { get; set; }

        [CustRequiredAttribute("fdrratedpower")]
        [Display(Name = "Rated Power")]  
        public Nullable<decimal> fdrratedpower { get; set; }

        [CustRequiredAttribute("fdrconnectedload")]
        [Display(Name = "Connected Load")]  
        public Nullable<decimal> fdrconnectedload { get; set; }

        [CustRequiredAttribute("fdrphase")]
        [Display(Name = "Feeder Phase")]  
        public string fdrphase { get; set; }

        [CustRequiredAttribute("fdrmanufacturerid")]
        [Display(Name = "Manufacturer Id")]  
        public string fdrmanufacturerid { get; set; }

        [CustRequiredAttribute("fdrsubstnid")]
        [Display(Name = "Sub Station")]  
        public string fdrsubstnid { get; set; }

        [CustRequiredAttribute("fdrinstallarea")]
        [Display(Name = "Install area")]
        public string fdrinstallarea { get; set; }
                
        [Display(Name = "Deleted")]  
        public Nullable<short> fdrisdeleted { get; set; }
    }
}