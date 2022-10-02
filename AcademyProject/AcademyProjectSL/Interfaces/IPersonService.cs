using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPersons { get; }

        Person? AddPerson(Person user);
        Person? DeletePerson(int userId);
        Person? GetById(int id);
        Person? UpdatePerson(Person user);
        Guid GetId();
    }
}
