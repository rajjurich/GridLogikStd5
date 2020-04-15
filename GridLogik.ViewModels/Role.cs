using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class Role
    {
        public Role()
        {
            this.Logins = new HashSet<Login>();
        }

        public long Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        [Display(Name = "Role Name")]
        public string Roles { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
