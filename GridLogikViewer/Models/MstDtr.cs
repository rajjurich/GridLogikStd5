using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstDtr
    {
        [Display(Name = "DTRRecId")]
        public long dtrrecid { get; set; }

        [CustRequiredAttribute("dtrid")]
        [Display(Name = "DTR Id")]
        [Pad]
        public string dtrid { get; set; }

        [CustRequiredAttribute("dtrname")]
        [Display(Name = "Name")]
        public string dtrname { get; set; }

        [CustRequiredAttribute("dtrdescription")]
        [Display(Name = "Description")]
        public string dtrdescription { get; set; }

        [CustRequiredAttribute("dtrratedvoltage")]
        [Display(Name = "RatedVoltage")]
        public string dtrratedvoltage { get; set; }

        [CustRequiredAttribute("dtrratedpower")]
        [Display(Name = "RatedPower")]
        public string dtrratedpower { get; set; }

        [CustRequiredAttribute("dtrconnectedload")]
        [Display(Name = "ConnectedLoad")]
        public string dtrconnectedload { get; set; }

        [CustRequiredAttribute("dtrinstallarea")]        
        [Display(Name = "InstallArea")]
        public string dtrinstallarea { get; set; }

        [CustRequiredAttribute("dtrserialno")]
        [Display(Name = "SerialNo")]
        public string dtrserialno { get; set; }

        [CustRequiredAttribute("dtrphase")]
        [Display(Name = "Phase")]
        public int dtrphase { get; set; }

        [CustRequiredAttribute("dtrmanufacturerid")]
        [Display(Name = "Manufacturer ID")]
        public string dtrmanufacturerid { get; set; }

        [CustRequiredAttribute("dtrfeederid")]
        [Display(Name = "Feeder")]
        public string dtrfeederid { get; set; }        

        [Display(Name = "DTRIsDeleted")]
        public bool dtrisdeleted { get; set; }
    }
}