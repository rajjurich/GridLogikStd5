using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class InputData
    {

        public Int16 IpNo { get; set; }

        [Display(Name = "Parameter Name")]
        [Required(ErrorMessage = "Parameter name is required")]
        public string ParaName { get; set; }

        [Display(Name = "Parameter Value")]
        [Required(ErrorMessage = "Parameter value is required")]
        public double ParaValue { get; set; }
        [Required(ErrorMessage = "Condition Value is required")]
        public string Condition { get; set; }

        public List<InputData> InputDataList { get; set; }

        public int? MeterID { get; set; }
        [Display(Name = "Multiple Select")]
        [Required(ErrorMessage = "Select Meter")]
        public virtual string[] MultipleMeterId { get; set; }

        public int? ParameterId { get; set; }
        [Display(Name = "Multiple Select")]
        [Required(ErrorMessage = "Select Parameter")]
        public virtual string[] MultipleParameterId { get; set; }

        public int? InputDataID { get; set; }
        [Display(Name = "Multiple Select")]
        [Required(ErrorMessage = "Select Input Data")]
        public virtual string[] MultipleInputDataId { get; set; }
        public int? OperatorId { get; set; }
        public int? JoinId { get; set; }
        public string ParameterValue { get; set; }

        public bool IsDeleted { get; set; }
    }
}
