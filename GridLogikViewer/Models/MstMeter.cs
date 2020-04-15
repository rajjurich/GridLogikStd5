using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstMeter
    {
        [Display(Name = "mtrrecid")]
        public long mtrrecid { get; set; }

        [CustRequiredAttribute("mtrno")]
        [Display(Name = "Meter no")]
        [Pad]
        public string mtrno { get; set; }

        [CustRequiredAttribute("mtrcommid")]
        [Display(Name = "Meter Communication ID")]
        public int mtrcommid { get; set; }

        [CustRequiredAttribute("mtrnetworkid")]
        [Display(Name = "Meter Network ID")]
        public string mtrnetworkid { get; set; }

        [CustRequiredAttribute("mtrtypeid")]
        [Display(Name = "Meter Type")]
        public string mtrtypeid { get; set; }

        [CustRequiredAttribute("mtrcommode")]
        [Display(Name = "Meter ComMode")]
        public string mtrcommode { get; set; }

        [CustRequiredAttribute("mtrcomprotocol")]
        [Display(Name = "Meter ComProtocol")]
        public string mtrcomprotocol { get; set; }

        [CustRequiredAttribute("mtroperatingchannel")]
        [Display(Name = "Meter Operating Channel")]
        public string mtroperatingchannel { get; set; }

        [CustRequiredAttribute("mtrinstallarea")]
        [Display(Name = "Meter Install Area")]
        public string mtrinstallarea { get; set; }

        [CustRequiredAttribute("mtrserialno")]
        [Display(Name = "Meter Serial No")]
        public string mtrserialno { get; set; }

        [CustRequiredAttribute("mtrhwversion")]
        [Display(Name = "Meter HW Version")]
        public string mtrhwversion { get; set; }

        [CustRequiredAttribute("mtrfwversion")]
        [Display(Name = "Meter FW Version")]
        public string mtrfwversion { get; set; }

        [CustRequiredAttribute("mtrmode")]
        [Display(Name = "Meter Mode")]
        public string mtrmode { get; set; }

        [CustRequiredAttribute("mtrnetworksecurity")]
        [Display(Name = "Meter Network Security")]
        public string mtrnetworksecurity { get; set; }

        [CustRequiredAttribute("mtrdtrid")]
        [Display(Name = "Meter DTR")]
        public string mtrdtrid { get; set; }

        [CustRequiredAttribute("mtrdcuid")]
        [Display(Name = "Meter DCU")]
        public string mtrdcuid { get; set; }


        [Display(Name = "mtrisdeleted")]
        public bool mtrisdeleted { get; set; }
    }
}