using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class Meter
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Please Enter Meter Name")]
        [Display(Name = "Meter Name")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9_:.,$;]+$", ErrorMessage = "First character must be a alphabet & special characters are not allow")]
        public string MeterName { get; set; }

        [Required(ErrorMessage = "Select Meter Type")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Select Meter Type")]
        [Display(Name = "Meter Type")]
        public long? MeterTypeID { get; set; }
        [Required(ErrorMessage = "Please Select Meter Model")]
        [Display(Name = "Model ID")]
        public long? ModelID { get; set; }


        [Required(ErrorMessage = "Please Select Communication Type")]
        [Display(Name = "Communication Type")]
        public long? CommunicationTypeId { get; set; }
        [Required(ErrorMessage = "Please Select Meter Group")]
        public long? GroupId { get; set; }
        [Display(Name = "Phase")]
        [Required(ErrorMessage = "Please Select Phase")]
        public string Phase { get; set; }
        [Display(Name = "Serial No.")]
        [Required(ErrorMessage = "Please Enter Serial No.")]
        public string SerialNo { get; set; }
        [Display(Name = "Meter No.")]
        [Required(ErrorMessage = "Please Enter Meter No.")]
        public string MeterNo { get; set; }
        public Nullable<short> isdeleted { get; set; }

        [Display(Name = "Modbus ID")]
        [Required(ErrorMessage = "Please Enter Modbus ID")]
        public Nullable<long> modbusid { get; set; }

        [Display(Name = "Primary")]
        [Required(ErrorMessage = "Please Enter Primary")]
        public Nullable<long> ctprimary { get; set; }

        [Display(Name = "Secondary")]
        [Required(ErrorMessage = "Please Enter Secondary")]
        public Nullable<long> ctsecondary { get; set; }
        public virtual ICollection<Alarm> Alarms { get; set; }
        public virtual ICollection<CommunicationDetail> CommunicationDetails { get; set; }
        public virtual CommunicationType CommunicationType { get; set; }
        public virtual ICollection<InstanceData> InstanceDatas { get; set; }
        public virtual ICollection<InstanceDataAverageLog> InstanceDataAverageLogs { get; set; }
        public virtual ICollection<InstanceDataLog> InstanceDataLogs { get; set; }
        public virtual ICollection<LoadService> LoadServices { get; set; }

        public List<ParameterMF> parametermfs { get; set; }
        public virtual MeterGroup MeterGroup { get; set; }
        public virtual MeterModel MeterModel { get; set; }
        public virtual MeterType MeterType { get; set; }

        [Display(Name = "Blockwise Data Available")]
        public short? blockwisedata { get; set; }

        [Display(Name = "Location")]
        //public string LocationDp { get; set; }
        public string Location { get; set; }

        [Display(Name = "LoadSurvey Flag")]
        public int? caltype { get; set; }

        [Display(Name = "Is Active")]
        public short? isactive { get; set; }
        public Nullable<short> misemail { get; set; }
        public Nullable<short> mispopup { get; set; }
        public Nullable<short> missms { get; set; }
        public bool StatusChecked
        {
            get { return blockwisedata == 1; }
            set { blockwisedata = value ? (short)1 : (short)0; }
        }

        public bool ActiveChecked
        {
            get { return isactive == 1; }
            set { isactive = value ? (short)1 : (short)0; }
        }
        public bool EmailChecked
        {
            get { return misemail == 1; }
            set { misemail = value ? (short)1 : (short)0; }
        }

        public bool PopChecked
        {
            get { return mispopup == 1; }
            set { mispopup = value ? (short)1 : (short)0; }
        }
        public bool SmsChecked
        {
            get { return missms == 1; }
            set { missms = value ? (short)1 : (short)0; }
        }
        public bool tagforsubmeter { get; set; }
        public long? replacedby { get; set; }


        [Display(Name = "New Meter")]
        public string parentcmrmeterid { get; set; }
        [Display(Name = "Replacement Remark")]
        public string replacementremark { get; set; }

        [Display(Name = "Rollover Limit")]
        //[RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Rollover Limit cannot start with 0 and Rollover Limit grater than 0")]
        //[Required(ErrorMessage = "Please Enter Rollover Limit")]
        public Nullable<long> rolloverlimit { get; set; }

        [Display(Name = "Parameter")]
        [Required(ErrorMessage = "Please Enter Parameter")]
        public string parameter { get; set; }
    }

    public class MeterVM
    {
        public long ID { get; set; }
        public string MeterName { get; set; }
    }

}
