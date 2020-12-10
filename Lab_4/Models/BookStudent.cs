using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class BookStudent
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
