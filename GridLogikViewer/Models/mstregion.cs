using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstRegion
    {
        public long RgnRecID { get; set; }

        [Display(Name = "Code")]
        [CustRequiredAttribute("RgnID")]
        [Pad]
        public string RgnID { get; set; }

        [Display(Name = "Name")]
        [CustRequiredAttribute("RgnName")]
        public string RgnName { get; set; }

        [Display(Name = "Zone")]
        [CustRequiredAttribute("RgnZnID")]        
        public string RgnZnID { get; set; }

        [Display(Name = "Description")]
        [CustRequiredAttribute("RgnDescription")]        
        public string RgnDescription { get; set; }
        
        public Nullable<bool> RgnIsDeleted { get; set; }
        
    }
}