using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstEmployee
    {
        public long emprecid { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Please Enter Code")]
        public string empid { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter First Name")]
        public string empfirstname { get; set; }
        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Please Enter Middle Name")]
        [CustRequiredAttribute("empmiddlename")]
        public string empmiddlename { get; set; }
        [Display(Name = "Last Name")]

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string emplastname { get; set; }

        public Nullable<short> empisactive { get; set; }
        public Nullable<short> empisdeleted { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]

        [Display(Name = "Email ID")]
     
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email ID")]
        public string empemailid { get; set; }

        //[RegularExpression(@"^(\+?\d{1,3}-\d{1,8}-\d{10})+$", ErrorMessage = "Invalid Phone Number")]

        [Display(Name = "Phone 1")]
        public string empphoneno1 { get; set; }
        [Display(Name = "Phone 2")]
      //  [RegularExpression(@"^(\+?\d{1,3}-\d{1,8}-\d{10})+$", ErrorMessage = "Invalid Phone Number")]
        public string empphoneno2 { get; set; }

    }
}