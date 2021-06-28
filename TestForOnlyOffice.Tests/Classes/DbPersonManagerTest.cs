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
    public class DbPersonManagerTest : PersonManagerTest
    {
        public ApplicationDbContext DbContext = new ApplicationDbContext();

        [OneTimeSetUp]
        public virtual void CreateDb()
        {
            DbContext.Database.EnsureCreated();
            _personManager = new DbPersonManager(DbContext);
        }

        [OneTimeTearDown]
        public virtual void DropDb()
        {
            DbContext.Database.EnsureDeleted();
        }

        [Test]
        public override void CreatePersonTest()
        {
            base.CreatePersonTest();
        }

        [Test]
        [TestCaseSource(nameof(_guidForDelete))]
        public override void DeletePersonTest(Guid id)
        {
            base.DeletePersonTest(id);
        }

        [Test]
        public override void GetPersonListTest()
        {
            base.GetPersonListTest();
        }

        [Test]
        [TestCaseSource(nameof(_guidForGetPerson))]
        public override void GetPersonTest(Guid id)
        {
            base.GetPersonTest(id);
        }

        [Test]
        [TestCaseSource(nameof(_person))]
        public override void UpdatePersonTest(Person person)
        {
            base.UpdatePersonTest(person);
        }
    }
}
