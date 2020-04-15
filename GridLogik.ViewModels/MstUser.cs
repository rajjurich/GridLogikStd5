using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MstUser
    {
        public long usrrecid { get; set; }

        [Display(Name = "User ID")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Please Enter User ID")]
        //   [RegularExpression(@"^[A-z0-9]+$", ErrorMessage = "Invalid User ID")]
        public string usrid { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(15)]
        [Display(Name = "Password")]
        public string usrpassword { get; set; }

        [MaxLength(15)]
        [Display(Name = "Confirm Password")]
        [Compare("usrpassword")]

        [Required(ErrorMessage = "Confirm Password is required")]
        //[Required(ErrorMessage = "The Password and Confirmation Password do not Match.")]
        public string usrRepassword { get; set; }

        [Required(ErrorMessage = "Please Select Role")]
        [Display(Name = "Role")]
        public string usrroleid { get; set; }

        //[Required(ErrorMessage = "Please Select Employee")]
        [Display(Name = "Employee")]
        public string usremployeeid { get; set; }
        public Nullable<short> usrisactive { get; set; }
        public Nullable<System.DateTime> usrcreationdate { get; set; }
        public bool isactive { get; set; }
        public string usrcreatedbyuserid { get; set; }
        //[Required(ErrorMessage = "Please Enter First Name")]

        [MaxLength(15)]
        [Display(Name = "First Name")]
        public string usrfirstname { get; set; }
        [Display(Name = "Middle Name")]
        [MaxLength(15)]
        public string usrmiddlename { get; set; }

        [MaxLength(15)]
        [Display(Name = "Last Name")]
        public string usrlastname { get; set; }

        [Display(Name = "Email ID")]
        [MaxLength(50)]
        public string usremailid { get; set; }

        [Display(Name = "Phone 1")]
        [MaxLength(20)]
        public string usrphoneno1 { get; set; }

        [Display(Name = "Phone 2")]
        [MaxLength(20)]
        public string usrphoneno2 { get; set; }
        public string usrtype { get; set; }
        public Nullable<short> usrisdeleted { get; set; }
        [Required(ErrorMessage = "Please Enter Old Password")]
        public string Old_Password { get; set; }
        //[Display(Name = "Locations")]
        //[Required(ErrorMessage = "Please Select Location")]
        //public List<int> locations { get; set; }
        //[Display(Name = "Groups")]
        //[Required(ErrorMessage = "Please Select Group")]
        //public List<int> groups { get; set; }
    }
}
