using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Models
{
    public class mstrolemenuaccess
    {

        public long rmarecid { get; set; }

        [Display(Name = " Role")]
    //    [Required(ErrorMessage = "Please enter Role")]
        public string rmaroleid { get; set; }

        [Display(Name = "Menu")]
    //    [Required(ErrorMessage = "Please select Menu")]
        public string rmamnuid { get; set; }

        [Display(Name = "Create")]
   //   [Required(ErrorMessage = "Please enter Description")]
        public bool rmacreateaccess { get; set; }

        [Display(Name = "Read")]
   //   [Required(ErrorMessage = "Please enter Description")]
        public bool rmareadaccess { get; set; }

        [Display(Name = "Update")]
   //   [Required(ErrorMessage = "Please enter Description")]
        public bool rmaupdateaccess { get; set; }


        [Display(Name = "Delete")]
   //   [Required(ErrorMessage = "Please enter Description")]
        public bool rmadeleteaccess { get; set; }

        public Nullable<bool> rmaisdeleted { get; set; }
    }
}



