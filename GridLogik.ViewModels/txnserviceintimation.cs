using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class txnserviceintimation
    {
        public long id { get; set; }

        [Display(Name = "From Date")]
        [Required(ErrorMessage = "Please select From Date")]
        //public System.DateTime fromdate { get; set; }
        public string fromdate { get; set; }

        [Display(Name = "To Date")]
        [Required(ErrorMessage = "Please select To Date")]
        //public System.DateTime todate { get; set; }
        public string todate { get; set; }

        [Required(ErrorMessage = "Please select Consumer Category")]
        [Display(Name = "Consumer Category")]
        public string consumercatid { get; set; }
        public short flagprocess { get; set; }
        public short pdfflagprocess { get; set; }

        public bool flagprocessChecked
        {
            get { return flagprocess == 1; }
            set { flagprocess = value ? (short)1 : (short)0; }
        }
    }
}
