using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GridLogik.ViewModels
{
    public class MstRegionModel
    {
        public long RgnRecID { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage="Required RgnID")]
        
        public string RgnID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required RgnName")]
        public string RgnName { get; set; }

        [Display(Name = "Zone")]
        [Required(ErrorMessage = "Required RgnZnID")]
        public string RgnZnID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required RgnDescription")]
        public string RgnDescription { get; set; }

        public Nullable<bool> RgnIsDeleted { get; set; }
    }
}
