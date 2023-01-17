using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer("server=DESKTOP-BFFPIIG\\SQLEXPRESS;database=ProjectDb;integrated security=true;");
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
