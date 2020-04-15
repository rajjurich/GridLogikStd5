using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class Consumer
    {
        [Display(Name = "Consumer Record ID")]
        public long id { get; set; }

        [Display(Name = "Consumer Master ID")]
        [StringLength(15, ErrorMessage = "Consumer Master ID must be maximum length of 15")]
        [Required(ErrorMessage = "Please enter Consumer Master ID")]
        public string masterid { get; set; }

        [Display(Name = "Consumer Name")]
        [Required(ErrorMessage = "Please enter Member Name")]
        public string firstname { get; set; }

        [Display(Name = "Middle Name")]
        public string middlename { get; set; }

        [Display(Name = "Last Name")]
        public string lastname { get; set; }

        [Display(Name = "Billing Unit")]
        public string billingunitid { get; set; }

        [Display(Name = "Class")]
        public string classid { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select Category")]
        public string categoryid { get; set; }

        [Display(Name = "User")]
        public string userid { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Marital Status")]
        public string maritalstatus { get; set; }

        [Display(Name = "DOB")]
        public string dob { get; set; }

        [Display(Name = "Office Address")]
        [DataType(DataType.MultilineText)]
        public string address1 { get; set; }

        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string address2 { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }

        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        [Display(Name = "Landline")]
        public string landline { get; set; }

        [Display(Name = "Mobile")]
        public string mobile { get; set; }

        [Display(Name = "Email Id")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Id")]
        public string emailid { get; set; }

        [Display(Name = "Education")]
        public string education { get; set; }

        [Display(Name = "Income range")]
        public string incomerange { get; set; }

        [Display(Name = "Occupation")]
        public string occupation { get; set; }

        [Display(Name = "Company Name")]
        public string companyname { get; set; }

        [Display(Name = "Contact Person")]
        public string contactperson { get; set; }

        [Display(Name = "Incorporation Date")]
        public string incorporationdate { get; set; }

        [Display(Name = "Incorporation Type")]
        public string incorporationtype { get; set; }

        [Display(Name = "Is Deleted")]
        public string isdeleted { get; set; }

        [Display(Name = "Type of use")]
        public string type { get; set; }
    }
}
