using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstConsumerClass
    {
        public long cclassrecid { get; set; }

        [Display(Name = "Code")]
        [CustRequiredAttribute("cclassid")]
        [Pad]
        public string cclassid { get; set; }

        [Display(Name = "Description")]
        [CustRequiredAttribute("cclassdescription")]
        public string cclassdescription { get; set; }

        [Display(Name = "Consumer Group")]
        [CustRequiredAttribute("cclassconsumergroup")]
        public string cclassconsumergroup { get; set; }
        public Nullable<short> cclassisdeleted { get; set; }
    }
}