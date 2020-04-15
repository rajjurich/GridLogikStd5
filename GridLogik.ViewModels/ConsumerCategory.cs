using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ConsumerCategory
    {
        
            public long categoryrecid { get; set; }

            [Display(Name = "Code")]
            //[CustRequiredAttribute("ccatid")] *
            //[Pad] *
            [Required(ErrorMessage = "Please enter Consumer Category Code")]
            [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Consumer Category Code")]
            public string categoryid { get; set; }

            [Display(Name = "Description")]
            //[CustRequiredAttribute("ccatdescription")]
            [Required(ErrorMessage = "Please enter Consumer Category Description")]
            public string categorydescription { get; set; }

            [Display(Name = "Tariff Code")]
            //[CustRequiredAttribute("cctariffid")]
            public string categorytariffid { get; set; }

            public Nullable<short> categoryisdeleted { get; set; }
            public bool checkcategoryfixedstatus { get; set; }
            public Nullable<int> categoryfixedstatus { get; set; }
    }
}
