using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class User
    {

        public enum UserType 
        { 
            [Display(Name = "Administrator")]
            Administrator,
            [Display(Name = "Student")]
            Student
        }


        public int UserId { get; set; }
        
        public UserType Type { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
