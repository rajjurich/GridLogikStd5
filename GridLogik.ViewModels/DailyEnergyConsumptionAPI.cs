using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class DailyEnergyConsumptionAPI
    {
        public List<MeterGroup> MeterGroup { get; set; }

        [Display(Name = "Meter Group")]
        public string MeterGroupId { get; set; }
    }
}
