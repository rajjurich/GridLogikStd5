using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class CommunicationDetail
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Select Communication Type")]
        [Display(Name = "Communication Type")]
        public long CommunicationTypeID { get; set; }
        //[Required(ErrorMessage = "Select Meter")]
        //[Range(0, Int32.MaxValue, ErrorMessage="Numbers Only")]
        //[Display(Name = "Meter")]
        //public long MeterId { get; set; }
        [Required(ErrorMessage = "Please Enter Ip Address")]
        //[RegularExpression(@"^(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$", ErrorMessage="Not valid IP Address")]
        //[RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Not valid IP Address")]
        [RegularExpression(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$", ErrorMessage = "Not valid IP Address")]
        [Display(Name = "Ip Address")]
        public string IpAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Port Number")]
        [Display(Name = "Port Number")]
        [Range(1, 65535, ErrorMessage = "Not a valid Port Number")]
        public string Port { get; set; }
        // [Required(ErrorMessage = "Please Enter Modbus Address")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid Modbus Address")]
        [Display(Name = "Modbus Address")]
        public string ModbusID { get; set; }
        [Required(ErrorMessage = "Please Enter Boud Rate")]

        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid Baud Rate")]
        [Display(Name = "Baud Rate")]
        public string BaudRate { get; set; }
        [Required(ErrorMessage = "Please Enter Data Bits")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid Data Bits")]
        [Display(Name = "Data Bits")]
        public string Databits { get; set; }
        [Required(ErrorMessage = "Please Enter Parity Bits")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Parity")]
        // [Range(0, Int32.MaxValue, ErrorMessage = "Invalid Parity")]
        [Display(Name = "Parity Bits")]
        public string Parity { get; set; }
        [Required(ErrorMessage = "Please Enter Stop Bits")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid Stop Bits")]
        [Display(Name = "Stop Bits")]
        public string StopBits { get; set; }

        [RegularExpression(@"^(?![\W_]+$)(?!\d+$)[a-zA-Z0-9 .&',_-]+$", ErrorMessage = "Invalid Convertor Name")]
        [Required(ErrorMessage = "Please Enter converter Id")]
        [Display(Name = "Convertor")]
        public string convertordesc { get; set; }

        [Display(Name = "Communication Type")]
        public string communicationtypename { get; set; }
        public string Metername { get; set; }

        [Required(ErrorMessage = "Please Enter Comp Port")]
        [Display(Name = "Comp Port")]
        public string comport { get; set; }

        [Display(Name = "Is Active")]
        public short isactive { get; set; }

        [Display(Name = "Is Active")]
        public bool ActiveChecked
        {
            get { return isactive == 1; }
            set { isactive = value ? (short)1 : (short)0; }
        }


        public Nullable<short> isemailalarm { get; set; }

        [Display(Name = "Email Alarm")]
        public bool emailalarmChecked
        {
            get { return isemailalarm == 1; }
            set { isemailalarm = value ? (short)1 : (short)0; }
        }

        public Nullable<short> ispopupalarm { get; set; }
        [Display(Name = "Popup Alarm")]
        public bool popupalarmChecked
        {
            get { return ispopupalarm == 1; }
            set { ispopupalarm = value ? (short)1 : (short)0; }
        }

        public Nullable<short> issmsalarm { get; set; }
        [Display(Name = "SMS Alarm")]
        public bool smsalarmChecked
        {
            get { return issmsalarm == 1; }
            set { issmsalarm = value ? (short)1 : (short)0; }
        }
    }
}
