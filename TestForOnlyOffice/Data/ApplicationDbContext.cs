using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Model;
using TestForOnlyOffice.Logging;

namespace TestForOnlyOffice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public static readonly ILoggerFactory FileLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new FileLoggerProvider("eflog"));
        });

        public DbSet<Person> Person { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(b => b.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.LastName).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.Email).IsRequired();
            modelBuilder.Entity<Person>().Property(b => b.Password).IsRequired();

            modelBuilder.Entity<Person>().HasData(new List<Person>
            {
                new Person
                {
                    Id = new Guid("f9278c77-4a5c-407b-95db-2d8736f2cd6c"),
                    FirstName = "Alex",
                    LastName = "Mokhov",
                    Email = "alexmokhov@ya.ru",
                    Password = "123456",
                    Language = "ru"
                },
                new Person
                {
                    Id = new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0"),
                    FirstName = "Vladimir",
                    LastName = "Mukhin",
                    Email = "mukhiv@ya.ru",
                    Password = "123456",
                    Language = "en"
                },
                new Person
                {
                    Id = new Guid("1f459835-ea0e-4047-b7a5-250ee512d691"),
                    FirstName = "Max",
                    LastName = "Novikov",
                    Email = "novikov@ya.ru",
                    Password = "123456",
                    Language = null
                },
                new Person
                {
                    Id = new Guid("963fea18-f18e-48da-967a-012c8ce6c06a"),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivanoff@ya.ru",
                    Password = "123456",
                    Language = null
                }
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 25))
            );
            optionsBuilder.UseLoggerFactory(FileLoggerFactory);
        }
    }
}
