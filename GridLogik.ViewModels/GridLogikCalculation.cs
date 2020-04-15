using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class GridLogikCalculation
    {
        public long id { get; set; }
        [Required(ErrorMessage = "Tag Name required")]
        [StringLength(150, ErrorMessage = "150 Character allowed")]
        [Display(Name="Tag Name")]
        public string tagname { get; set; }
        [Required(ErrorMessage = "Query required")]
        [Display(Name = "Query")]
        public string query { get; set; }
        [Display(Name = "Calculated value")]
        public Nullable<double> value { get; set; }
        [Display(Name = "Calculation date and time")]
        public Nullable<System.DateTime> tstamp { get; set; }
        public string datatype { get; set; }
        public string parameter { get; set; }
        public string tablename { get; set; }

        public bool istag { get; set; }  

    }
}
