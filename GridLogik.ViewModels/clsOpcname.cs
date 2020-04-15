using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class clsOpcname
    {
        public long id { get; set; }
        [Required(ErrorMessage="Please Select Meter")]
        public long meterid { get; set; }
        public string mopname { get; set; }
        [Required(ErrorMessage="Please Enter OPC Name")]
        public string opc_shortname { get; set; }
        public Nullable<short> isdeleted { get; set; }
    }
}
