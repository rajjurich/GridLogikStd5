using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class CommunicationType
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Enter Communication Type")]
        [Display(Name = "Communication Type")]
        public string CommunicationType1 { get; set; }

        public virtual ICollection<CommunicationDetail> CommunicationDetails { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
    }
}
