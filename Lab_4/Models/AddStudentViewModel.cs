using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class AddStudentViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Student first name can not be empty")]
        [Display(Name = "Student First Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "First name length have to be from 4 to 30")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Student last name can not be empty")]
        [Display(Name = "Student Last Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Second name length have to be from 4 to 30")]
        public string SecondName { get; set; }
       
        [Required(ErrorMessage = "Student father name can not be empty")]
        [Display(Name = "Student Father Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Father name length have to be from 4 to 30")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Student group can not be empty")]
        [Display(Name = "Student Group")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Group name length have to be from 7 to 12")]
        public string Group { get; set; }

        [Required(ErrorMessage = "Student faculty can not be empty")]
        [Display(Name = "Student Faculty")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "Faculty name length have to be from 2 to 8")]
        public string Faculty { get; set; }

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
