using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class prmglobal
    {
        [Display(Name = "Id")]
        public long prmrecid { get; set; }
        [Required(ErrorMessage="Module required")]
        [Display(Name = "Module")]
        public string prmmodule { get; set; }
        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Unit required")]
        public string prmunit { get; set; }
        [Display(Name = "Identifier")]
        [Required(ErrorMessage = "Identifier required")]
        public string prmidentifier { get; set; }
        [Display(Name = "Value")]
        [Required(ErrorMessage = "Value required")]
        public string prmvalue { get; set; }
        public string rfu1 { get; set; }
        public string rfu2 { get; set; }

    }
}
