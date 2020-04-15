using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterType
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Please Enter Meter Type")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Meter Type")]
        public string MeterTypeName { get; set; }

        [Display(Name = "Meter Description")]
        [Required(ErrorMessage = "Please Enter Meter Description")]
        public string meterdescription { get; set; }
        public Nullable<short> isdeleted { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
    }
}
