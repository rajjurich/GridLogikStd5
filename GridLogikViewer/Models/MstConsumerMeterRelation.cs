using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Models
{
    public class MstConsumerMeterRelation
    {
        const  string strDate ="";
        public long cmrrecid { get; set; }
        [StringLength(15, ErrorMessage = "Consumer Code must be maximum length of 15")]
        [RegularExpression("^[a-zA-Z0-9._]*$", ErrorMessage = "Consumer Code must be Alpha Numeric")]
        [Display(Name = "Meter Assign  Code")]
        [CustRequiredAttribute("cmrconsumerid")]
        [Pad]
        [Required(ErrorMessage = "Please Enter Consumer Code")]
        public string cmrconsumerid { get; set; }

        [Display(Name = "Consumer")]
        [Required(ErrorMessage = "Please Select Consumer")]
        [CustRequiredAttribute("cmrconsumermasterid")]
        public string cmrconsumermasterid { get; set; }
     
        [Display(Name = " Meter")]
        [Required(ErrorMessage = "Please Select Meter")]
       // [RegularExpression("^((?!Select Meter).)*$", ErrorMessage = "Please Select Meter")]
       // [CustRequiredAttribute("cmrmeterid")]
        public string cmrmeterid { get; set; }

        [Display(Name = " Start Date")]
        [Required(ErrorMessage = "Please Select  Start Date")]
       // [DisplayFormat(DataFormatString = strDate, ApplyFormatInEditMode = true)]
        [CustRequiredAttribute("cmrrelationshipstartdate")]
        public string cmrrelationshipstartdate { get; set; }

        [Display(Name = " End Date")]
       // [Required(ErrorMessage = "Please Select  End Date")]
        [CustRequiredAttribute("cmrrelationshipenddate")]
        [DateGreaterThan("cmrrelationshipstartdate")]
        public string cmrrelationshipenddate { get; set; }

        [Display(Name = " Service Start Date")]
      //  [Required(ErrorMessage = "Please Service  Start Date")]
        [CustRequiredAttribute("cmrservicestartdate")]
        public string cmrservicestartdate { get; set; }

        [Display(Name = " Service End Date")]
       // [Required(ErrorMessage = "Please Service  End Date")]
        [CustRequiredAttribute("cmrserviceenddate")]
        [DateGreaterThan("cmrservicestartdate")]
        public string cmrserviceenddate { get; set; }
        public Nullable<short> cmrisdeleted { get; set; }

        [Display(Name = " Tag For Submeter")]
        //[CustRequiredAttribute("tagforsubmeter")]
        public bool tagforsubmeter { get; set; }


         [Display(Name = "Parent Meter")]
        public string parentcmrmeterid { get; set; }
        [Display(Name = "Connected Load")]
         public Nullable<double> cmrcurrconnloadkw { get; set; }
        [Display(Name = "sanction Load")]
         public Nullable<double> cmdcurrcontractdemandkva { get; set; }
    }
}