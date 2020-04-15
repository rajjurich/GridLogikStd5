using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class ClientFolderMap
    {
        public long id { get; set; }
        [Display(Name="Role")]
        [Required]
        public Nullable<int> roleid { get; set; }
        public string RoleName { get; set; }
        [Display(Name = "Path")]
        [Required]
        public string folderpath { get; set; }
    }
}
