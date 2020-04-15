using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ActionType
    {
        public long id { get; set; }
        [Required(ErrorMessage = "Please select the alarm")]
        public long alarmid { get; set; }
        [Required(ErrorMessage = "Please select the alarm type")]
        public string alarmtype { get; set; }


        public string mobileno { get; set; }

        public string message { get; set; }

        public string emailaddress { get; set; }

        //public virtual Alarm Alarm { get; set; }

        [Required(ErrorMessage = "Please enter Mobile No")]
        public virtual string[] mobilenos { get; set; }

        [Required(ErrorMessage = "Please enter Email ID")]
        public virtual string[] emailaddresses { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Id")]
        public string txtEmailId { get; set; }

        public short isdeleted { get; set; }

        public string Ipaddress { get; set; }
    }
}
