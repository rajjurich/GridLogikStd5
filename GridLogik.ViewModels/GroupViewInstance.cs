using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class GroupViewInstance
    {

        [Display(Name = "Meter Name")]
        [Required(ErrorMessage = "Meter name is required")]
        public int MeterId { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group name is required")]
        public int GroupId { get; set; }

        public List<MeterGroup> Groups { get; set; }
        public List<MeterVM> Meters { get; set; }

        [Display(Name = "Parameter")]
        public List<MeterVM> parameterlist { get; set; }
    }
}
