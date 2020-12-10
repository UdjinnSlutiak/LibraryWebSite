using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class Student : User
    {

        [Display(Name = "Group")]
        public string Group { get; set; }

        [Display(Name = "Faculty")]
        public string Faculty { get; set; }
        
        
        public List<BookStudent> BooksStudent { get; set; } = new List<BookStudent>();



        public void TakeBook(Book book)
        {
            /* if (!Books.Contains(book) && book.IsAvailable)
             {
                 Books.Add(book);
                 book.Students.Add(this);
                 return true;
             }
             return false;*/
            this.BooksStudent.Add( new BookStudent { BookId = book.Id, StudentId = this.UserId });

        }

        public void ReturnBook(Book book)
        {
            /*if (Books.Contains(book))
            {
                Books.Remove(book);
                book.Students.Remove(this);
            }*/
            if (book != null)
            {
                var bookStudent = this.BooksStudent.FirstOrDefault(bs => bs.BookId == book.Id);
                this.BooksStudent.Remove(bookStudent);
            }
        }
    }
}
