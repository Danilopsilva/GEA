using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEA.Employees.Models
{
    public class Operator
    {   
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedWhen { get; set; }
        public int CreatedById { get; set; }
    }
}