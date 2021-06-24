using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Classes;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using System.Text.Json;
using TestForOnlyOffice.Model;
using System.Collections.Generic;

namespace TestForOnlyOffice.Tests
{
    [SetUpFixture]
    public class MySetUpDbClass
    {
        protected IServiceScope Scope { get; set; }

        [OneTimeSetUp]
        public virtual void CreateDb()
        {
            var host = Program.CreateHostBuilder(new string[] {
                "--pathToConf", Path.Combine("..", "..", "..", "..","..", "..", "config"),
                "--ConnectionStrings:default:connectionString", BaseDataTests.TestConnection,
                "--migration:enabled", "true",
                "--core:products:folder", Path.Combine("..", "..", "..", "..","..", "..", "products")}).Build();

            Migrate(host.Services);
            Migrate(host.Services, Assembly.GetExecutingAssembly().GetName().Name);

            Scope = host.Services.CreateScope();
        }

        [OneTimeTearDown]
        public virtual void DropDb()
        {
            var context = Scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

        private void Migrate(IServiceProvider serviceProvider, string testAssembly = null)
        {
            using var scope = serviceProvider.CreateScope();

            if (!string.IsNullOrEmpty(testAssembly))
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                configuration["testAssembly"] = testAssembly;
            }

            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            db.Migrate();
        }
    }

    public class BaseDataTests
    {
        protected IServiceScope scope { get; set; }
        public const string TestConnection = "server=localhost;port=3306;user=root;password=Wki4bJCC5CsQtpl5PV;database=testdb_test;";

        public virtual void SetUp()
        {
            var host = Program.CreateHostBuilder(new string[] {
                "--pathToConf" , Path.Combine("..", "..", "..", "..","..", "..", "config"),
                "--ConnectionStrings:default:connectionString", TestConnection,
                 "--migration:enabled", "true" }).Build();

            scope = host.Services.CreateScope();
        }
    }

    [SetUpFixture]
    public class MySetUpJsonClass
    {
        [OneTimeSetUp]
        public virtual void CreateJson()
        {
            List<Person> personsList = new List<Person>
            {
                new Person
                {
                    PersonId = new Guid("f9278c77-4a5c-407b-95db-2d8736f2cd6c"),
                    FirstName = "Alex",
                    LastName = "Mokhov",
                    Email = "alexmokhov@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    PersonId = new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0"),
                    FirstName = "Vladimir",
                    LastName = "Mukhin",
                    Email = "mukhiv@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    PersonId = new Guid("1f459835-ea0e-4047-b7a5-250ee512d691"),
                    FirstName = "Max",
                    LastName = "Novikov",
                    Email = "novikov@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    PersonId = new Guid("963fea18-f18e-48da-967a-012c8ce6c06a"),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivanoff@ya.ru",
                    Password = "123456"
                }
            };
            string json = JsonSerializer.Serialize(personsList);
            File.WriteAllText("person.json", json);
        }

        [OneTimeTearDown]
        public virtual void DropJson()
        {
            File.Delete("person.json");
        }
    }
}