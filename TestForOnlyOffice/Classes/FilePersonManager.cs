using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Data;
using TestForOnlyOffice.Interfaces;
using TestForOnlyOffice.Model;
using System.Text.Json;
using System.IO;

namespace TestForOnlyOffice.Classes
{
    public class FilePersonManager : IPersonManager
    {
        List<Person> _personList;

        public FilePersonManager()
        {
            _personList = GetPersonList();
        }

        public void Create(Person person)
        {
            _personList = GetPersonList();
            _personList.Add(person);
            string jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
        }

        public void Delete(Guid id)
        {
            Person person = _personList.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                _personList.Remove(person);
            }
            string jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
        }

        public Person GetPerson(Guid id)
        {
            string jsonStr = File.ReadAllText("person.json");
            var personList = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonStr).ToList();
            return personList.FirstOrDefault(x => x.Id == id);
        }

        public List<Person> GetPersonList()
        {
            string jsonStr = File.ReadAllText("person.json");
            var personList = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonStr).ToList();
            return personList;
        }

        public Person Update(Person person)
        {
            _personList = GetPersonList();
            var recordPerson = new Person();
            foreach (var record in _personList)
            {
                if (record.Id == person.Id)
                {
                    record.FirstName = person.FirstName;
                    record.LastName = person.LastName;
                    record.Email = person.Email;
                    record.Password = record.Password;
                    recordPerson = record;
                }
            }
            var jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
            return recordPerson;
        }
    }
}
