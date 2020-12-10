using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class GetViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Administrator> Administrators { get; set; }
        
        public User User;

        public Book Book;
        
        public Student Student = new Student();

        public Administrator Administrator = new Administrator();
    }
}
