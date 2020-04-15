using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ModuleModel
    {
        public long id { get; set; }

        [Display(Name = "Module Name")]
        [Required(ErrorMessage = "Please Enter Module Name")]
        public string modulename { get; set; }

        [Display(Name = "Module Position")]
        [Required(ErrorMessage = "Please Enter Module Position")]
      // [MinLength(1)]
       // [RegularExpression("^[0-9]*$", ErrorMessage = "Module Position must be numeric")]
        public long moduleposition { get; set; }
        public string link { get; set; }
        public Nullable<short> isdeleted { get; set; }

        public virtual ICollection<MstMenuModels> mstmenus { get; set; }
    }
}
