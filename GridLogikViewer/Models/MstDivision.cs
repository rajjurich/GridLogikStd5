using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstDivision
    {
        public long divrecid { get; set; }

        [Display(Name = "Code")]
        [CustRequiredAttribute("divid")]
        [Pad]
        public string divid { get; set; }

        [Display(Name = "Name")]
        [CustRequiredAttribute("divname")]
        public string divname { get; set; }

        [Display(Name = "Description")]
        [CustRequiredAttribute("divdescription")]
        public string divdescription { get; set; }

        [Display(Name = "Region")]
        [CustRequiredAttribute("divrgnid")]
        public string divrgnid { get; set; }
        public Nullable<short> divisdeleted { get; set; }

       
    }
}