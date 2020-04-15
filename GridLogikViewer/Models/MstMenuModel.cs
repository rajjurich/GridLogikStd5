using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstMenuModel
    {
        public long mnurecid { get; set; }

        [Display(Name = " Code")]
        [StringLength(6, ErrorMessage = "Menu Code must be 6 digit")]
        [Required(ErrorMessage = "Please Enter Menu Code")]
        
        public string mnuid { get; set; }

        [Display(Name = " Name")]
        [StringLength(20, ErrorMessage = "Menu Name must be maximum length of 20")]
        [Required(ErrorMessage = "Please Enter Menu Name")]
        [RegularExpression(@"^([a-zA-Z0-9]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z0-9]+)*))*$", ErrorMessage = "Menu Name must be Alpha Numeric with Single Space")]
        
        public string mnuitemname { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Please Enter Menu Position")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Numbers Only")]
        
        public string mnuitemposition { get; set; }

        public Nullable<bool> mnuisdeleted { get; set; }

        [Display(Name = "Module Name")]
        [Required(ErrorMessage = "Please Select Module")]
        public Nullable<bool> mnumodulid { get; set; }
        
        [Display(Name = "Menu Link")]
        [Required(ErrorMessage = "Please Enter Menu link")]

        
        
        
        public string link { get; set; }
    }
}