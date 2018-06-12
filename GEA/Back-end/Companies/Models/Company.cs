using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEA.Back_end.Companies.Models
{
    public class Company
    {   
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime CreatedWhen { get; set; }
    }
}