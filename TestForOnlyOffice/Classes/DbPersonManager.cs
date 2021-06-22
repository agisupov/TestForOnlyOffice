using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Classes
{
    public class DbPersonManager : IPersonManager
    {
        ApplicationDbContext _db;
        private bool disposed = false;

        public DbPersonManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Person person)
        {
            _db.Person.Add(person);
        }

        public void Delete(int id)
        {
            Person person = _db.Person.Find(id);
            if (person != null)
                _db.Person.Remove(person);
        }

        public Person GetPerson(int? id)
        {
            return _db.Person.Find(id);
        }

        public IEnumerable<Person> GetPersonList()
        {
            return _db.Person;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Person person)
        {
            _db.Entry(person).State = EntityState.Modified;
        }

        public bool PersonExists(int? id)
        {
            return _db.Person.Any(x => x.PersonId == id);
        }
    }
}
