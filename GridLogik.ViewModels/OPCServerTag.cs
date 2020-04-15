using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{

    public class OPCServerTag
    {
        public long id { get; set; }
        [Required(ErrorMessage = "Tag Name required")]
        [StringLength(150, ErrorMessage = "150 Character allowed")]
        [Display(Name="Tagname")]
        public string tagname { get; set; }
        [Required(ErrorMessage = "Tablename required")]
        [StringLength(50, ErrorMessage = "50 Character allowed")]
        [Display(Name = "Tablename")]
        public string tablename { get; set; }
        [Required(ErrorMessage = "Parameter required")]
        [StringLength(150, ErrorMessage = "150 Character allowed")]
        [Display(Name = "Parameter")]
        public string parameter { get; set; }
        [Required(ErrorMessage = "Datatype required")]        
        [Display(Name = "Datatype")]        
        public string datatype { get; set; }
        [Required(ErrorMessage = "Meter required")]
        [Display(Name = "Meter")]
        public Nullable<long> meterid { get; set; }
        [Display(Name = "Priority")]
        public Nullable<long> priority { get; set; }
        [Display(Name = "Priority")]
        public int _priority { get; set; }
        public Nullable<long> istag { get; set; }
        [Display(Name = "IsTag")]
        public bool is_tag { get; set; }        
        public string Metername { get; set; }
    }   
}
