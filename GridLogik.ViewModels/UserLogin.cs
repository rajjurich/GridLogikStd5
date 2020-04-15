using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GridLogik.ViewModels
{
    public class UserLogin
    {
        public long usrrecid { get; set; }

        [Display(Name = "User ID")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Please Enter User ID")]        
        public string usrid { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(15)]
        [Display(Name = "Password")]
        public string usrpassword { get; set; }

        [Required(ErrorMessage = "Captcha Required")]
        public string captchaResult { get; set; }
        [Required]
        public string oprtr { get; set; }
        [Required]
        public int firstNumber { get; set; }
        [Required]
        public int secondNumber { get; set; }

        public string verionsofwebapp { get; set; }
    }
}
