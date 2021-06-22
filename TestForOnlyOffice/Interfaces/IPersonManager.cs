using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOnlyOffice.Model;

namespace TestForOnlyOffice.Interfaces
{
    public interface IPersonManager
    {
        List<Person> GetPersonList();
        Person GetPerson(string id);
        bool PersonExists(string id);
        void Create(Person person);
        void Update(Person person);
        void Delete(string id);
    }
}
