using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestForOnlyOffice.Classes;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Tests.Classes
{
    [TestFixture]
    public class DbPersonManagerTest : MySetUpDbClass
    {
        public readonly IPersonManager _mockPersonManager;
        public ApplicationDbContext _db;

        public TestContext TestContext { get; set; }

        List<Person> _personsList = new List<Person>
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

        public DbPersonManagerTest()
        {
            //_db = new ApplicationDbContext();
            //_db.Person.AddRange(_personsList);
            //_db.SaveChanges();
            Mock<IPersonManager> mockPersonManager = new Mock<IPersonManager>();
            mockPersonManager.Setup(x => x.Create(It.IsAny<Person>()));
            mockPersonManager.Setup(x => x.GetPersonList()).Returns(_personsList);
            mockPersonManager.Setup(x => x.GetPerson(It.IsAny<Guid>()))
                .Returns((Guid guid) => _db.Person.FirstOrDefault(z => guid.Equals(z.PersonId)));

            mockPersonManager.Setup(x => x.Update(It.IsAny<Person>()))
                .Returns((Person person) => _db.Person.FirstOrDefault(z => person.PersonId.Equals(z.PersonId)));

            mockPersonManager.Setup(x => x.Delete(It.IsAny<Guid>()));

            _mockPersonManager = mockPersonManager.Object;
        }

        [Test]
        [Order(1)]
        public void GetPersonTest()
        {
            var id = _personsList[2].PersonId;
            Person person = _mockPersonManager.GetPerson(id);

            Assert.NotNull(person);
            Assert.AreEqual("Novikov", person.LastName);
        }

        [Test]
        [Order(2)]
        public void CreatePersonTest()
        {
            Person person = new Person
            {
                PersonId = new Guid("858f7f23-0dc5-43be-81a7-90fe835fa954"),
                FirstName = "Timofei",
                LastName = "Moskvin",
                Email = "moskvin@ya.ru",
                Password = "123456"
            };

            _mockPersonManager.Create(person);
            person = _mockPersonManager.GetPerson(person.PersonId);

            Assert.IsNotNull(person);
        }

        [Test]
        [Order(3)]
        public void UpdatePersonTest()
        {
            var id = new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0");
            Person person = _mockPersonManager.GetPerson(id);
            Assert.NotNull(person);

            person.LastName = "Pamann";
            person = _mockPersonManager.Update(person);

            Assert.NotNull(person);
            Assert.AreEqual("Pamann", person.LastName);
        }

        [Test]
        [Order(4)]
        public void GetPersonListTest()
        {
            List<Person> personsList = _mockPersonManager.GetPersonList();
            Assert.IsNotNull(personsList);
            Assert.AreEqual(4, personsList.Count);
        }

        [Test]
        [Order(5)]
        public void DeletePersonTest()
        {
            var id = new Guid("963fea18-f18e-48da-967a-012c8ce6c06a");
            _mockPersonManager.Delete(id);

            Assert.IsNull(_mockPersonManager.GetPerson(id));
        }
    }
}
