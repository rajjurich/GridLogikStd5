using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Models
{
    public class MstRole
    {

        public long rolrecid { get; set; }

        [Display(Name = " Code")]
        [Required(ErrorMessage = "Please Enter Role Code")]
        [StringLength(6,ErrorMessage="Role Code must be 6 digit") ]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Role Code must be Numeric")]
        [CustRequiredAttribute("rolid")]
        [Pad]
        public string rolid { get; set; }

        [Display(Name = " Name")]
        [Required(ErrorMessage = "Please Enter Role Name")]
        [StringLength(15, ErrorMessage = "Role Name must be maximum length of 15")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Role Name must be Alpha Numeric only")]
        [CustRequiredAttribute("rolname")]
        public string rolname { get; set; }

        [Display(Name = "Description")]
        [StringLength(25, ErrorMessage = "Description must be maximum length of 25.")]
        [RegularExpression(@"^([a-zA-Z0-9]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z0-9]+)*))*$", ErrorMessage = "Description must be Alpha Numeric with Single Space")]
        [CustRequiredAttribute("roldescription")]
        public string roldescription { get; set; }

        public Nullable<bool> rolisdeleted { get; set; }
    }
}