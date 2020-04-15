using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GridLogik.ViewModels
{
    public class MstDivisionModel
    {
        public long divrecid { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage="Div Id Required")]
        
        public string divid { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage="Divname Required")]
        public string divname { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage="Divdescription Required")]
        public string divdescription { get; set; }

        [Display(Name = "Region")]
        [Required(ErrorMessage="Divrgnid Required")]
        public string divrgnid { get; set; }
        public Nullable<short> divisdeleted { get; set; }
    }
}
