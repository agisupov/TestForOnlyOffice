using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestForOnlyOffice.Classes;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Tests.Classes
{
    [TestFixture]
    public class FilePersonManagerTest : PersonManagerTest
    {
        [OneTimeSetUp]
        public virtual void CreateJson()
        {
            List<Person> personsList = new List<Person>
            {
                new Person
                {
                    Id = new Guid("f9278c77-4a5c-407b-95db-2d8736f2cd6c"),
                    FirstName = "Alex",
                    LastName = "Mokhov",
                    Email = "alexmokhov@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    Id = new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0"),
                    FirstName = "Vladimir",
                    LastName = "Mukhin",
                    Email = "mukhiv@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    Id = new Guid("1f459835-ea0e-4047-b7a5-250ee512d691"),
                    FirstName = "Max",
                    LastName = "Novikov",
                    Email = "novikov@ya.ru",
                    Password = "123456"
                },
                new Person
                {
                    Id = new Guid("963fea18-f18e-48da-967a-012c8ce6c06a"),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivanoff@ya.ru",
                    Password = "123456"
                }
            };
            string json = JsonSerializer.Serialize(personsList);
            File.WriteAllText("person.json", json);
            _personManager = new FilePersonManager();
        }

        [OneTimeTearDown]
        public virtual void DropJson()
        {
            File.Delete("person.json");
        }
    }
}
