using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GridLogik.ViewModels
{
    public class MstSubstationModel
    {
        public long ssrecid { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage="Required Code")]
        
        public string ssid { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required Name")]
        public string ssname { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required Description")]
        public string ssdescription { get; set; }


        [Required(ErrorMessage = "Required Division")]
        [Display(Name = "Division")]
        public string ssdivid { get; set; }
        public Nullable<short> ssisdeleted { get; set; }
    }
}
