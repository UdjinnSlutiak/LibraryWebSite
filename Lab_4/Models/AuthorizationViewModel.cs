using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class AuthorizationViewModel
    {

        [Required]
        [Display(Name = "User Type")]
        public User.UserType Type { get; set; }

        [Required(ErrorMessage = "Login can not be empty")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
