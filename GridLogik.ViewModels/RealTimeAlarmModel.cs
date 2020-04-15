using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class RealTimeAlarmModel
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Please enter Alarm Name")]
        public string alarmname { get; set; }

        [Required(ErrorMessage = "Please enter Alarm Message")]
        public string alarmmessage { get; set; }
        public Nullable<System.DateTime> fromtime { get; set; }
        public Nullable<System.DateTime> totime { get; set; }
        public Nullable<int> duration { get; set; }
        public short status { get; set; }
        public Nullable<byte> resetstatus { get; set; }
        public string alarmquery { get; set; }

        [Required(ErrorMessage = "Please Insert Alarm Query")]
        public string condition { get; set; }
        public string alarmcommand { get; set; }

        public List<MeterDetails> MeterSelectionList { get; set; }

        //public bool SendSMS { get; set; }
        //public bool SendEmail { get; set; }
        //public bool GivePopup { get; set; }

        public short sendsms { get; set; }
        public short sendemail { get; set; }
        public short givepopup { get; set; }


        public string meterid { get; set; }


        public string ParameterID { get; set; }

        [Required(ErrorMessage = "Please select Meter(s)")]
        public virtual string[] multiplemeterID { get; set; }


        [Required(ErrorMessage = "Please select Parameter(s)")]
        public virtual string[] multipleparameters { get; set; }

        public short isdeleted { get; set; }
        public bool SendSMSChecked
        {
            get { return sendsms == 1; }
            set { sendsms = value ? (short)1 : (short)0; }
        }

        public bool SendEmailChecked
        {
            get { return sendemail == 1; }
            set { sendemail = value ? (short)1 : (short)0; }
        }

        public bool GivePopupChecked
        {
            get { return givepopup == 1; }
            set { givepopup = value ? (short)1 : (short)0; }
        }

        public bool StatusChecked
        {
            get { return status == 1; }
            set { status = value ? (short)1 : (short)0; }
        }

        public string parameter { get; set; }
    }
}
