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
            person.PersonId = Guid.NewGuid().ToString();
            _personList.Add(person);
            string jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
        }

        public void Delete(string id)
        {
            Person person = _personList.FirstOrDefault(x => x.PersonId == id);
            if (person != null)
            {
                _personList.ToList().Remove(person);
            }
            string jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
        }

        public Person GetPerson(string id)
        {
            return _personList.FirstOrDefault(x => x.PersonId == id);
        }

        public List<Person> GetPersonList()
        {
            string jsonStr = File.ReadAllText("person.json");
            var personList = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonStr).ToList();
            return personList;
        }

        public bool PersonExists(string id)
        {
            return _personList.Any(x => x.PersonId == id);
        }

        public void Update(Person person)
        {
            string jsonStr = File.ReadAllText("person.json");
            _personList = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonStr).ToList();
            foreach (var record in _personList)
            {
                if (record.PersonId == person.PersonId)
                {
                    record.FirstName = person.FirstName;
                    record.LastName = person.LastName;
                    record.Email = person.Email;
                    record.Password = record.Password;
                }
            }
            jsonStr = JsonSerializer.Serialize(_personList);
            File.WriteAllText("person.json", jsonStr);
        }
    }
}
