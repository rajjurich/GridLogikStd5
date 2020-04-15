using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstZone
    {
        public long znrecid { get; set; }

        [Display(Name = "Code")]
        [CustRequiredAttribute("znid")]
        [Pad]
        public string znid { get; set; }

        [Display(Name = "Name")]
        [CustRequiredAttribute("znname")]
        public string znname { get; set; }

        [Display(Name = "Description")]
        [CustRequiredAttribute("zndescription")]
        public string zndescription { get; set; }
        public Nullable<bool> znisdeleted { get; set; }

        [CustRequiredAttribute("znutilityid")]
        [Display(Name="Utility")]
        public string znutilityid { get; set; }
        
      
    }
}