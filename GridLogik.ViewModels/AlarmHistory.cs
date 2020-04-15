using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class AlarmHistory
    {
        public long ID { get; set; }
        public long AlarmId { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }

        public virtual Alarm Alarm { get; set; }
    }
}
