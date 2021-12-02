using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JobOpeningsDemo.Models
{
    public class JobsDBContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}