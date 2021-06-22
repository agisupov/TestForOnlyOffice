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

        public DbPersonManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Person person)
        {
            person.PersonId = Guid.NewGuid().ToString();
            _db.Person.Add(person);
            _db.SaveChanges();
        }

        public void Delete(string id)
        {
            Person person = _db.Person.Find(id);
            if (person != null)
            {
                _db.Person.Remove(person);
                _db.SaveChanges();
            }
        }

        public Person GetPerson(string id)
        {
            return _db.Person.Find(id);
        }

        public List<Person> GetPersonList()
        {
            return _db.Person.ToList();
        }

        public void Update(Person person)
        {
            var record = _db.Person.First(x => x.PersonId == person.PersonId);
            if (record != null)
            {
                record.FirstName = person.FirstName;
                record.LastName = person.LastName;
                record.Email = person.Email;
                record.Password = person.Password;
                _db.SaveChanges();
            }
        }

        public bool PersonExists(string id)
        {
            return _db.Person.Any(x => x.PersonId == id);
        }
    }
}
