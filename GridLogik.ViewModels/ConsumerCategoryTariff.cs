using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ConsumerCategoryTariff
    {
        public long consumercategoryrecid { get; set; }

        [Required(ErrorMessage = "Please select Consumer Category")]
        public string categoryid { get; set; }

        [Required(ErrorMessage = "Please select Tariff")]
        public string tariffid { get; set; }

        [Required(ErrorMessage = "Please enter From Date")]
        public string fromdate { get; set; }
        public System.DateTime todate { get; set; }
        public short isdeleted { get; set; }
    }
}
