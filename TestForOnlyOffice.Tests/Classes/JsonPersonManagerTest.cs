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
    public class JsonPersonManagerTest : MySetUpJsonClass
    {
        public readonly IPersonManager _mockPersonManager;
        List<Person> _personsList = new List<Person>();

        public JsonPersonManagerTest()
        {
            Mock<IPersonManager> mockPersonManager = new Mock<IPersonManager>();
            mockPersonManager.Setup(x => x.Create(It.IsAny<Person>()));
            mockPersonManager.Setup(x => x.GetPersonList()).Returns(_personsList);
            mockPersonManager.Setup(x => x.GetPerson(It.IsAny<Guid>()))
                .Returns((Guid guid) => _personsList.FirstOrDefault(z => guid.Equals(z.PersonId)));

            mockPersonManager.Setup(x => x.Update(It.IsAny<Person>()))
                .Returns((Person person) => _personsList.FirstOrDefault(z => person.PersonId.Equals(z.PersonId)));

            mockPersonManager.Setup(x => x.Delete(It.IsAny<Guid>()));

            _mockPersonManager = mockPersonManager.Object;
        }

        [OneTimeSetUp]
        public override void CreateJson()
        {
            base.CreateJson();
        }

        [OneTimeTearDown]
        public override void DropJson()
        {
            base.DropJson();
        }

        [Test]
        [Order(1)]
        public void GetPersonTest()
        {
            var list = _mockPersonManager.GetPersonList();
            var id = list[0].PersonId;
            Person person = _mockPersonManager.GetPerson(id);

            Assert.NotNull(person);
            Assert.AreEqual("Novikov", person.LastName);
        }

        [Test]
        [Order(2)]
        public void CreatePersonTest()
        {
            //Arrange
            Person person = new Person()
            {
                PersonId = new Guid("858f7f23-0dc5-43be-81a7-90fe835fa954"),
                FirstName = "Timofei",
                LastName = "Moskvin",
                Email = "moskvin@ya.ru",
                Password = "123456"
            };

            //Act
            _mockPersonManager.Create(person);
            person = _mockPersonManager.GetPerson(person.PersonId);
            //Assert
            Assert.NotNull(person);
        }

        [Test]
        [Order(3)]
        public void UpdatePersonTest()
        {
            var personList = _mockPersonManager.GetPersonList();
            var person = personList.FirstOrDefault(x => x.PersonId == new Guid("858f7f23-0dc5-43be-81a7-90fe835fa954"));
            person.LastName = "Nemoskvin";
            _mockPersonManager.Update(person);
            person = _mockPersonManager.GetPerson(person.PersonId);
            Assert.AreEqual("Nemoskvin", person.LastName);
        }

        [Test]
        [Order(4)]
        public void GetPersonListTest()
        {
            var personList = _mockPersonManager.GetPersonList();

            Assert.IsNotNull(personList);
            Assert.AreEqual(4, personList.Count);
        }

        [Test]
        [Order(5)]
        public void DeletePersonTest()
        {
            var personList = _mockPersonManager.GetPersonList();
            var person = personList.FirstOrDefault(x => x.PersonId == new Guid("858f7f23-0dc5-43be-81a7-90fe835fa954"));

            _mockPersonManager.Delete(person.PersonId);
            person = _mockPersonManager.GetPerson(person.PersonId);
            Assert.IsNull(person);
        }
    }
}
