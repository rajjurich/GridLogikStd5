using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Please Enter Meter Model Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Please Enter Alpha Number with Space in Model Name")]
        [Display(Name = "Meter Model Name")]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "Please Enter Meter Description")]
        [Display(Name = "Meter Description")]
        public string Description { get; set; }

        [Display(Name = "Rollover Limit")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Rollover Limit cannot start with 0 and Rollover Limit grater than 0")]
        [Required(ErrorMessage = "Please Enter Rollover Limit")]
        public Nullable<long> rolloverlimit { get; set; }

        public virtual ICollection<MemoryMap_Addressdetails> MemoryMap_Addressdetails { get; set; }
        public virtual ICollection<MemoryMap_Range> MemoryMap_Range { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
    }
}
