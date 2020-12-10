using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    public class Administrator : User
    {

        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}
