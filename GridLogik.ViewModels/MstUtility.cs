using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GridLogik.ViewModels
{
    public class MstUtility
    {

        public long utilrecid { get; set; }

        [Display(Name = "Code")]

        //[CustRequiredAttribute("utilid")]
        //[Pad]
        [MaxLength(10)]
        [Required(ErrorMessage = "Please Enter Code")]
        // [RegularExpression(@"^[A-z0-9]+$", ErrorMessage = "Invalid Code")]
        public string utilid { get; set; }

        [Display(Name = "Name")]

        //[CustRequiredAttribute("utilname")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter Name")]
        //[RegularExpression(@"^[A-z]+[0-9,',\-,\\,.]*$", ErrorMessage = "Invalid Name")]
        public string utilname { get; set; }

        [Display(Name = "Address1")]

        //[CustRequiredAttribute("utiladdress1")]
        [MaxLength(50)]
        public string utiladdress1 { get; set; }
        [Display(Name = "Address2")]

        //[CustRequiredAttribute("utiladdress2")]
        [MaxLength(50)]
        public string utiladdress2 { get; set; }

        [Display(Name = "Address3")]

        //[CustRequiredAttribute("utiladdress3")]
        [MaxLength(50)]
        public string utiladdress3 { get; set; }

        //[CustRequiredAttribute("utilpin")]

        [StringLength(6, ErrorMessage = "Max length is 6")]
        [Display(Name = "Pin")]
        [Required(ErrorMessage = "Please Enter Pin")]


        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Pin")]
        public string utilpin { get; set; }
        public Nullable<short> isdeleted { get; set; }
    }
}
