using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class AddAdministratorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Administrator first name can not be empty")]
        [Display(Name = "Administrator First Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "First name length have to be from 4 to 30")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Administrator last name can not be empty")]
        [Display(Name = "Administrator Last Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Second name length have to be from 4 to 30")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Administrator father name can not be empty")]
        [Display(Name = "Administrator Father Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Father name length have to be from 4 to 30")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Position can not be empty")]
        [Display(Name = "Position")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Position length have to be from 4 to 30")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Login can not be empty")]
        [Display(Name = "Login")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Login length have to be from 8 to 30")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        [Display(Name = "Password")]
        [RegularExpression(@"[A-Za-z0-9._]{6,20}", ErrorMessage = "Incorrect password")]
        public string Password { get; set; }

        public User User { get; set; }
    }
}
