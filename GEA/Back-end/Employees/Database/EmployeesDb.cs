using GEA.Employees.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace GEA.Back_end.Employees.Database
{
    public class EmployeesDb : DbContext
    {
        DbSet<Operator> Operations { get; set; }

        public System.Data.Entity.DbSet<GEA.Employees.Models.Operator> Operators { get; set; }
    }
}