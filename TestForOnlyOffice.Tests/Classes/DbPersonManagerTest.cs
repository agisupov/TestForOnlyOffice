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
            _personManager = new DbPersonManager(DbContext);
        }

        [OneTimeTearDown]
        public virtual void DropDb()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}
