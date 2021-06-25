using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Data
{
    public class TestDbContext : BaseDbContext
    {
        public DbSet<Person> Person { get; set; }

        public TestDbContext()
        {
            Database.EnsureCreated();
        }

        public TestDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(b => b.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.LastName).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.Email).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.Password).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;user=root;password=Wki4bJCC5CsQtpl5PV;database=testdb_test;",
                new MySqlServerVersion(new Version(8, 0, 25))
            );
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
