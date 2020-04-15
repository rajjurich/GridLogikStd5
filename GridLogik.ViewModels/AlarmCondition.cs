using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class AlarmCondition
    {
        public long Id { get; set; }
        public string Parameter { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public Nullable<long> AlarmId { get; set; }

        public virtual Alarm Alarm { get; set; }
    }
}
