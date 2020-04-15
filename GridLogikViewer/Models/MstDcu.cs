using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstDcu
    {
        [Display(Name = "DCU RecID")]
        public long dcurecid { get; set; }

        [CustRequiredAttribute("dcuid")]
        [Display(Name = "DCU ID")]
        [Pad]
        public string dcuid { get; set; }

        [CustRequiredAttribute("dcucomid")]
        [Display(Name = "DCU ComID")]
        public decimal dcucomid { get; set; }

        [CustRequiredAttribute("dcupanid")]
        [Display(Name = "DCU PANID")]
        public decimal dcupanid { get; set; }

        [CustRequiredAttribute("dcumetercommode")]
        [Display(Name = "DCU Meter ComMode")]        
        public string dcumetercommode { get; set; }

        [CustRequiredAttribute("dcumdascommode")]
        [Display(Name = "DCU MDAS ComMode")]        
        public string dcumdascommode { get; set; }

        [CustRequiredAttribute("dcumetercomprotocol")]
        [Display(Name = "DCU Meter ComProtocol")]        
        public string dcumetercomprotocol { get; set; }

        [CustRequiredAttribute("dcumdascomprotocol")]
        [Display(Name = "DCU MDAS ComProtocol")]        
        public string dcumdascomprotocol { get; set; }

        [CustRequiredAttribute("dcuoperatingchannel")]
        [Display(Name = "DCU Operating Channel")]
        public int dcuoperatingchannel { get; set; }

        [CustRequiredAttribute("dcuinstallarea")]
        [Display(Name = "DCU Install Area")]        
        public string dcuinstallarea { get; set; }

        [Display(Name = "DCU IsDeleted")]
        public bool dcuisdeleted { get; set; }
    }
}