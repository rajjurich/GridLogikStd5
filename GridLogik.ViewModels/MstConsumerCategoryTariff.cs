using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MstConsumerCategoryTariff
    {
        public long cctrecid { get; set; }

        [Required(ErrorMessage = "Please select Consumer Category")]
        public string cctcategoryid { get; set; }

        [Required(ErrorMessage = "Please select Tariff")]
        public string ccttariffid { get; set; }

        [Required(ErrorMessage = "Please enter From Date")]
        public string cctfromdate { get; set; }
        public System.DateTime ccttodate { get; set; }
        public short cctisdeleted { get; set; }
    }
}
