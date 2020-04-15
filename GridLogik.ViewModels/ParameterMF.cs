using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ParameterMF
    {
        [Required]
        public long id { get; set; }

        [Required]
        public Nullable<long> meterid { get; set; }
        [Required]
        public string grouptype { get; set; }
        [Required]
        public Nullable<double> multiplicationfactor { get; set; }
    }
}
