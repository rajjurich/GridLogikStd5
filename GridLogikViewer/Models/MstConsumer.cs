using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstConsumer
    {
        [Display(Name = "Consumer Record ID")]
        public long csmrrecid { get; set; }
        
        [Display(Name = "Consumer Master ID")]
        [StringLength(15, ErrorMessage = "Consumer Master ID must be maximum length of 15")]
        [Required(ErrorMessage = "Please enter Consumer Master ID")]
        public string csmrmasterid { get; set; }
        
        [Display(Name = "Consumer Name")]
        [Required(ErrorMessage = "Please enter Member Name")]
        public string csmrfirstname { get; set; }

        [Display(Name = "Middle Name")]
        public string csmrmiddlename { get; set; }

        [Display(Name = "Last Name")]
        public string csmrlastname { get; set; }

        [Display(Name = "Billing Unit")]
        public string csmrbillingunitid { get; set; }

        [Display(Name = "Class")]
        public string csmrclassid { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select Category")]
        public string csmrcategoryid { get; set; }

        [Display(Name = "User")]
        public string csmruserid { get; set; }

        [Display(Name = "Gender")]
        public string csmrgender { get; set; }

        [Display(Name = "Marital Status")]
        public string csmrmaritalstatus { get; set; }

        [Display(Name = "DOB")]
        public string csmrdob { get; set; }

        [Display(Name = "Office Address")]
        [DataType(DataType.MultilineText)]
        public string csmraddress1 { get; set; }

        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string csmraddress2 { get; set; }

        [Display(Name = "City")]
        public string csmrcity { get; set; }

        [Display(Name = "State")]
        public string csmrstate { get; set; }

        [Display(Name = "Country")]
        public string csmrcountry { get; set; }

        [Display(Name = "Pincode")]
        public string csmrpincode { get; set; }

        [Display(Name = "Landline")]
        public string csmrlandline { get; set; }

        [Display(Name = "Mobile")]
        public string csmrmobile { get; set; }

        [Display(Name = "Email Id")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Id")]
        public string csmremailid { get; set; }

        [Display(Name = "Education")]
        public string csmreducation { get; set; }
                
        [Display(Name = "Income range")]
        public string csmrincomerange { get; set; }
        
        [Display(Name = "Occupation")]
        public string csmroccupation { get; set; }
        
        [Display(Name = "Company Name")]
        public string csmrcompanyname { get; set; }
        
        [Display(Name = "Contact Person")]
        public string csmrcontactperson { get; set; }
                
        [Display(Name = "Incorporation Date")]
        public string csmrincorporationdate { get; set; }
        
        [Display(Name = "Incorporation Type")]
        public string csmrincorporationtype { get; set; }

        [Display(Name = "Is Deleted")]
        public string csmrisdeleted { get; set; }

        [Display(Name = "Type of use")]
        public string csmrtype { get; set; }
    }
}