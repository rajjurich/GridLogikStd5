using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Models
{
    public class MstConsumerCategory
    {

        public long ccatrecid { get; set; }

        [Display(Name = "Code")]        
        //[CustRequiredAttribute("ccatid")] *
        //[Pad] *
        [Required(ErrorMessage = "Please enter Consumer Category Code")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Consumer Category Code")]
        public string ccatid { get; set; }

        [Display(Name = "Description")]
        //[CustRequiredAttribute("ccatdescription")]
        [Required(ErrorMessage = "Please enter Consumer Category Description")]
        public string ccatdescription { get; set; }

        [Display(Name = "Tariff Code")]
        //[CustRequiredAttribute("cctariffid")]
        public string cctariffid { get; set; }

        public Nullable<short> ccatisdeleted { get; set; }
    }
}