using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Tests.Classes
{
    [TestFixture]
    public abstract class PersonManagerTest
    {
        public IPersonManager _personManager;
        public static List<Person> _person = new List<Person>()
        {
            new Person(){
            Id = new Guid("1f459835-ea0e-4047-b7a5-250ee512d691"),
            FirstName = "Max",
            LastName = "Novikov",
            Email = "novikov@ya.ru",
            Password = "123456",
            Language = "en"
            }
        };
        public static List<Guid> _guidForDelete = new List<Guid>()
        {
            new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0")
        };
        public static List<Guid> _guidForGetPerson = new List<Guid>()
        {
            new Guid("f9278c77-4a5c-407b-95db-2d8736f2cd6c")
        };

        [Test]
        [TestCaseSource(nameof(_guidForGetPerson))]
        public void GetPersonTest(Guid id)
        {
            Person person = _personManager.GetPerson(id);

            Assert.NotNull(person);
            Assert.AreEqual("Mokhov", person.LastName);
        }

        [Test]
        public void CreatePersonTest()
        {
            //Arrange
            Person person = new Person()
            {
                Id = new Guid("858f7f23-0dc5-43be-81a7-90fe835fa954"),
                FirstName = "Timofei",
                LastName = "Moskvin",
                Email = "moskvin@ya.ru",
                Password = "123456",
                Language = "en"
            };

            //Act
            _personManager.Create(person);
            person = _personManager.GetPerson(person.Id);
            //Assert
            Assert.NotNull(person);
        }

        [Test]
        [TestCaseSource(nameof(_person))]
        public void UpdatePersonTest(Person person)
        {
            person.LastName = "Nemoskvin";
            _personManager.Update(person);
            person = _personManager.GetPerson(person.Id);
            Assert.AreEqual("Nemoskvin", person.LastName);
        }

        [Test]
        public void GetPersonListTest()
        {
            var personList = _personManager.GetPersonList();

            Assert.IsNotNull(personList);
            Assert.AreEqual(4, personList.Count);
        }

        [Test]
        [TestCaseSource(nameof(_guidForDelete))]
        public void DeletePersonTest(Guid id)
        {
            var personList = _personManager.GetPersonList();
            var person = personList.FirstOrDefault(x => x.Id == id);

            _personManager.Delete(person.Id);
            person = _personManager.GetPerson(person.Id);
            Assert.IsNull(person);
        }
    }
}
