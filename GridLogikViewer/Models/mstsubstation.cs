using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstSubstation
    {
        public long ssrecid { get; set; }

        [Display(Name = "Code")]
        [CustRequiredAttribute("ssid")]
        [Pad]
        public string ssid { get; set; }

        [Display(Name = "Name")]
        [CustRequiredAttribute("ssname")]
        public string ssname { get; set; }

        [Display(Name = "Description")]
        [CustRequiredAttribute("ssdescription")]
        public string ssdescription { get; set; }


        [CustRequiredAttribute("ssdivid")]
        [Display(Name = "Division Name")]
        public string ssdivid { get; set; }
        public Nullable<short> ssisdeleted { get; set; }


    }
}