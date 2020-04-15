using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GridLogikViewer.CustomValidation;

namespace GridLogikViewer.Models
{
    public class MstTOUSlot
    {
        public long tsrecid { get; set; }
        public string tstouid { get; set; }
        public string tsslotno { get; set; }
        [Required(ErrorMessage="Please enter")]
        public string tsslotstart { get; set; }

        [CustRequiredAttribute("tsslotend")]
        public string tsslotend { get; set; }
        public string tsmaxdemandlimit { get; set; }
        public Nullable<short> tsisdeleted { get; set; }
        public int NoOfRows { get; set; }
    }
}