using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class AddBtSViewModel
    {

        public Student Student { get; set; }
        
        public List<Book> AvailableBooks { get; set; }
        
        public IEnumerable<Book> TakenBooks { get; set; }
    }
}
