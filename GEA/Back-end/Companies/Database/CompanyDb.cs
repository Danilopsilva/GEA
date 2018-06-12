using GEA.Back_end.Companies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GEA.Back_end.Companies.Database
{
    public class CompanyDb : DbContext
    {
        public DbSet<Company> Companies { get; set; }
    }
}