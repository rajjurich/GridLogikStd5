using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{

    public class GroupConfiguration
    {
        public long Id { get; set; }
        public Nullable<long> GroupId { get; set; }
        public Nullable<long> MeterId { get; set; }
        public Nullable<long> ParameterId { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group name is required")]
        [RegularExpression("^[a-zA-Z0-9 _]+$", ErrorMessage = "Group name should be alphanumeric")]
        public string GroupName { get; set; }

        public string MeterName { get; set; }
        public Nullable<long> Meter { get; set; }
        public string Parameter { get; set; }

        public virtual MeterGroup MeterGroup { get; set; }
        public virtual Meter Meter1 { get; set; }
        [Display(Name = "Multiple Select")]
        [Required(ErrorMessage = "Select Meter")]
        public virtual string[] multiplemeterID { get; set; }
        [Display(Name = "Multiple Select")]
        [Required(ErrorMessage = "Select Parameter")]
        public virtual string[] InstanceParameterID { get; set; }
        public virtual string[] SelectedMeterID { get; set; }
        public virtual string[] SelectedParameterID { get; set; }
        // public virtual InstanceData InstanceData { get; set; }

    }

}
