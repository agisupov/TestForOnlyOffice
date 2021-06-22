using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Interfaces
{
    public interface IPersonManager
    {
        IEnumerable<Person> GetPersonList();
        Person GetPerson(int? id);
        bool PersonExists(int? id);
        void Create(Person person);
        void Update(Person person);
        void Delete(int id);
        void Save();
    }
}
