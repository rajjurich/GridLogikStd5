using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstTariff
    {
        public long trfrecid { get; set; }

        [Required(ErrorMessage="Please enter Tariff Code")]
        [Display(Name = "Code")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Tariff Code")]
      //  [CustRequiredAttribute("trfid")]

        //[Pad]
        public string trfid { get; set; }

        [Display(Name = "TOU Code")]
       // [CustRequiredAttribute("trftouid")]      
        public string trftouid { get; set; }

        [Display(Name = "TOU Slot")]
        //[CustRequiredAttribute("trftouslotid")]    
        public string trftouslotid { get; set; }

        [Display(Name = "Tariff Scheme Name")]
        [Required(ErrorMessage = "Please enter Tariff Scheme Name")]
      //  [CustRequiredAttribute("trfschemename")]    
        public string trfschemename { get; set; }
        public Nullable<short> trfisdeleted { get; set; }

       // [CustRequiredAttribute("znutilityid")]
        [Required(ErrorMessage="Please select Utility")]
        [Display(Name = "Utility")]
        public string znutilityid { get; set; }

        /////////////////
       // [CustRequiredAttribute("trffromdate")]
        [Display(Name = "Tarrif From Date")]
        [Required(ErrorMessage="Please enter From Date")]
        //public DateTime trffromdate { get; set; }
        public string trffromdate { get; set; }

       // [CustRequiredAttribute("trftodate")]
        public DateTime trftodate { get; set; }

        
    }


    
}