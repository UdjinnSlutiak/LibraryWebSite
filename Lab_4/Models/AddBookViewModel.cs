using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class AddBookViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Book name can not be empty")]
        [Display(Name = "Book Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name length have to be from 2 to 40")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Author name can not be empty")]
        [Display(Name = "Author Name")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Name length have to be from 8 to 30")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Page count can not be empty")]
        [Display(Name = "Page Count")]
        [Range(1, 1500, ErrorMessage = "Incorrect page count")]
        public int PageCount { get; set; }


        [Required (ErrorMessage = "Total count can not be empty)")]
        [Display(Name = "Books Count")]
        [Range(1, 10, ErrorMessage = "Total count have to be from 1 to 10")]
        public int TotalCount { get; set; }

        public User User;
    }
}
