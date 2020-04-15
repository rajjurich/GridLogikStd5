using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace GridLogikViewer.Models
{
    public class mstmodcostdata
    {


    
        public long mrecid { get; set; }
        [Display(Name = "Generator")]
        [Required(ErrorMessage = "Please Select Generator Name")]
        public long mgenid { get; set; }
        [Display(Name = "Fixed Cost")]
        [Required(ErrorMessage = "Please Enter Fixed Cost")]
        public Nullable<double> mfixct { get; set; }
        [Display(Name = "Linear cost")]
        [Required(ErrorMessage = "Please Enter Linear cost")]
        public Nullable<double> mlnct { get; set; }
        [Display(Name = "Quadratic  cost")]
        [Required(ErrorMessage = "Please Enter Quadratic cost")]
        public Nullable<double> mqdct { get; set; }
        [Display(Name = "Start Up")]
        [Required(ErrorMessage = "Please Enter Start Up")]
        [Range(0, 50000, ErrorMessage = "Start Up Cost Cannot be negative value")]
        public Nullable<double> mstartup { get; set; }
        [Display(Name = "Shut Down")]
        [Required(ErrorMessage = "Please Enter Shut Down")]
        [Range(0, 50000, ErrorMessage = "Shut Down Cost Cannot be negative value")]
        public Nullable<double> mshut { get; set; }
        [Display(Name = "Max Ramping Limit")]
        [Required(ErrorMessage = "Please Enter Max Ramping Limit")]
        [Range(0, 50000, ErrorMessage = "Max Ramping Limit Cannot be negative value")]
        public Nullable<double> mmrl { get; set; }
        [Display(Name = "Min Up Duration")]
        [Required(ErrorMessage = "Please Enter Min Up Duration")]
        [Range(0, 96, ErrorMessage = "Please enter a value between 0 to 96")]
        public Nullable<long> mmud { get; set; }
        [Display(Name = "Min Down Duration")]
        [Required(ErrorMessage = "Please Enter Min Down Duration")]
        [Range(0, 96, ErrorMessage = "Please enter a value between 0 to 96")]
        public Nullable<long> mmdd { get; set; }
        [Display(Name = "Current State")]
        [Required(ErrorMessage = "Please Select Current State of Unit")]
        public Nullable<long> mstate { get; set; }
        [Display(Name = "Schedule")]
        [Required(ErrorMessage = "Please Enter Schedule")]
        [Range(0, 50000, ErrorMessage = "Schedule Value Cannot be negative")]
        public Nullable<double> mschdule { get; set; }
        [Display(Name = "No. of blocks in current state")]
        [Required(ErrorMessage = "Please Enter No. of blocks in current state")]
        [Range(0, 96, ErrorMessage = "Please enter a value between 0 to 96")]
        public Nullable<double> mdurcst { get; set; }

        public string metername { get; set; }
    }
}