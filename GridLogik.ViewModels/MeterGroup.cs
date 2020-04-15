using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MeterGroup
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter group name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [Required(ErrorMessage="Meters required")]
        public List<MeterGroupDetail> mstmetergroupdetails { get; set; }

    }
}
