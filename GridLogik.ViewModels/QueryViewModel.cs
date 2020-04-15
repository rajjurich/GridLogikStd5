using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GridLogik.ViewModels
{
    public class QueryViewModel
    {
        [Display(Name = "Tables")]
        [Required(ErrorMessage = "Table Name Required")]
        public string TableName { get; set; }
        [Required(ErrorMessage = "Columns Required")]
        public List<string> Columns { get; set; }        
        public List<string> Meters { get; set; }
        [Display(Name = "Interval")]
        //[Required(ErrorMessage = "Interval Required")]
        public string Interval { get; set; }
        [Required(ErrorMessage = "From Time Required")]
        public string FromTime { get; set; }
        [Required(ErrorMessage = "To Time Required")]
        public string ToTime { get; set; }

        [AllowHtml]
        public string Csv { get; set; } 
    }
}
