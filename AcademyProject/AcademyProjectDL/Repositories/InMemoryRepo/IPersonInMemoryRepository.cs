using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IPersonInMemoryRepository
    {
        IEnumerable<Person> GetAllPersons { get; }
        Person? AddPerson(Person user);
        Person? DeletePerson(int userId);
        Person? GetById(int id);
        Person? UpdatePerson(Person user);
        Guid GetId();
    }
}