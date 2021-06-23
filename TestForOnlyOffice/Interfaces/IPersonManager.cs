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
        Person GetPerson(Guid id);
        bool PersonExists(Guid id);
        void Create(Person person);
        Person Update(Person person);
        void Delete(Guid id);
    }
}
