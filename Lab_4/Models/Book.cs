using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class Book
    {


        public int Id { get; set; }

        [Display(Name = "Book Name")]
        public string Name { get; set; }

        [Display(Name = "Author Name")]
        public string Author { get; set; }

        [Display(Name = "Page Count")]
        public int PageCount { get; set; }

        [Display(Name = "Books Count")]
        public int TotalCount { get; set; }
        
        
        [NotMapped]
        public int AvailableCount { get => TotalCount - BookStudents.Count(); }
        
        
        [NotMapped]
        public bool IsAvailable { get => AvailableCount > 0; }
        
        
        public List<BookStudent> BookStudents { get; set; } = new List<BookStudent>();
    }
}
