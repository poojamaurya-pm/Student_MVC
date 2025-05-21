using Student_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Student_MVC.DataContext
{
    public class StudentContextDb: DbContext
    {
        public StudentContextDb() : base("EfEmployee") { }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}