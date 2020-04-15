using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GridLogik.ViewModels
{
    public class MstZoneModel
    {
        public long znrecid { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage="Please Provide Code")]
        public string znid { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage="Please Provide Name")]
        public string znname { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage="Please Provide Description")]
        public string zndescription { get; set; }
        public Nullable<bool> znisdeleted { get; set; }

        
        [Display(Name = "Utility")]
        [Required(ErrorMessage="Utility Id Required")]
        public string znutilityid { get; set; }
    }
}
