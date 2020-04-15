using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class GroupCallViewModel
    {
        public int ID { get; set; }
        public int meterid { get; set; }

        [Display(Name = "Group Name")]
        public string groupname { get; set; }
        public string metername { get; set; }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }
        public string StartTime { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }
        public string EndTime { get; set; }
        public string Param { get; set; }

    }
}
