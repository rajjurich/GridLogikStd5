using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GridLogik.ViewModels
{
    public class StandardAlarmModel
    {
        public long id { get; set; }
        public int? meterid { get; set; }
        [Required(ErrorMessage = "Please select Meters")]
        public virtual string[] multiplemeterID { get; set; }

        [Required(ErrorMessage = "Please enter max value for Voltage")]
        public Nullable<double> maxvll { get; set; }
        [Required(ErrorMessage = "Please enter min value for Voltage")]
        public Nullable<double> minvll { get; set; }
        [Required(ErrorMessage = "Please enter max value for Current")]
        public Nullable<double> maxamp { get; set; }
        [Required(ErrorMessage = "Please enter min value for Current")]
        public Nullable<double> minamp { get; set; }
        [Required(ErrorMessage = "Please enter max value for Active Power")]
        public Nullable<double> maxkw { get; set; }
        [Required(ErrorMessage = "Please enter min value for Active Power")]
        public Nullable<double> minkw { get; set; }
        [Required(ErrorMessage = "Please enter max value for kVA")]
        public Nullable<double> maxkva { get; set; }
        [Required(ErrorMessage = "Please enter min value for kVA")]
        public Nullable<double> minkva { get; set; }
        [Required(ErrorMessage = "Please enter max value for Frequency")]
        public Nullable<double> maxhz { get; set; }
        [Required(ErrorMessage = "Please enter min value for Frequency")]
        public Nullable<double> minhz { get; set; }
        [Required(ErrorMessage = "Please enter max value for PF")]
        public Nullable<double> maxpf { get; set; }
        [Required(ErrorMessage = "Please enter MIN value for PF")]
        public Nullable<double> minpf { get; set; }
        [Required(ErrorMessage = "Please enter Connect Load")]
        public Nullable<double> connectload { get; set; }
        [Required(ErrorMessage = "Please enter Alarm Message")]
        public string alarmmessages { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public short? status { get; set; }
        public Nullable<int> resetstatus { get; set; }
        public Nullable<int> duration { get; set; }

        public string alarmcommand { get; set; }

        public List<Meter> MeterSelectionList { get; set; }

        public short? sendsms { get; set; }
        public short? sendemail { get; set; }
        public short? givepopup { get; set; }

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

        public short isdeleted { get; set; }

        [Required(ErrorMessage = "Please enter Alarm Name")]
        public string alarmname { get; set; }

        public string MeterName { get; set; }
    }
}
