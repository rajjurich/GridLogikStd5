using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class Alarm
    {
        public long Id { get; set; }
        public long MeterID { get; set; }
        [Required(ErrorMessage = "Please enter alarm name")]
        public string AlarmName { get; set; }
        [Required(ErrorMessage = "Please enter message")]
        public string Message { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual ICollection<ActionType> ActionTypes { get; set; }
        public virtual List<AlarmCondition> AlarmConditions { get; set; }
        public virtual ICollection<AlarmHistory> AlarmHistories { get; set; }
        public virtual Meter Meter { get; set; }
    }
}
