using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Login
{
    public class LoginModel
    {
        public LoginModel() 
        {
            RememberMe = false;
            IsHumen = false;
        }
        public int id { get; set; }
        [MaxLength(200)]
        [Required]
        public string UserName { get; set; }
        //public string PhoneNumber { get; set; }

        [MaxLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string CaptchaImage { get; set; }
        public string Captcha { get; set; }
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
        public bool IsHumen { get; set; }
    }
}
