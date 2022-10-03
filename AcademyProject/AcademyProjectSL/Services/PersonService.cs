using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonInMemoryRepository _data;

        public PersonService(IPersonInMemoryRepository repo)
        {
            _data = repo;
        }

        public IEnumerable<Person> GetAllPersons => _data.GetAllPersons;

        public Person? AddPerson(Person user)
        {
            _data.AddPerson(user);
            return user;
        }

        public Person? DeletePerson(int userId)
        {
            return _data.DeletePerson(userId);
        }

        public Person? GetById(int id)
        {
            return _data.GetById(id);
        }

        public Guid GetId()
        {
            return _data.GetId();
        }

        public Person? UpdatePerson(Person user)
        {
            return _data.UpdatePerson(user);
        }
    }
}