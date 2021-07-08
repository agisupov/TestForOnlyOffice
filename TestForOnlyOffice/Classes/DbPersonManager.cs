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

        public async void Create(Person person)
        {
            _db.Person.Add(person);
            await _db.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            Person person = _db.Person.Find(id);
            if (person != null)
            {
                _db.Person.Remove(person);
                _db.SaveChanges();
            }
        }

        public Person GetPerson(Guid id)
        {
            return _db.Person.Find(id);
        }

        public List<Person> GetPersonList()
        {
            return _db.Person.ToList();
        }

        public Person Update(Person person)
        {
            var record = _db.Person.FirstOrDefault(x => x.Id == person.Id);
            if (record != null)
            {
                if (record.FirstName != person.FirstName)
                    record.FirstName = person.FirstName;

                if (record.LastName != person.LastName)
                    record.LastName = person.LastName;

                if (record.Email != person.Email)
                    record.Email = person.Email;

                if (record.Password != person.Password)
                    record.Password = person.Password;

                if (record.Language != person.Language)
                    record.Language = person.Language;

                _db.SaveChanges();
            }
            return record;
        }

        public Person Login(string email, string password)
        {
            return _db.Person.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
