using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonInMemoryRepository _personInMemoryRepo;

        public PersonService(IPersonInMemoryRepository repo)
        {
            _personInMemoryRepo = repo;
        }

        public IEnumerable<Person> GetAllPersons => _personInMemoryRepo.GetAllPersons;

        public Person? AddPerson(Person user)
        {
            _personInMemoryRepo.AddPerson(user);
            return user;
        }

        public Person? DeletePerson(int userId)
        {
            return _personInMemoryRepo.DeletePerson(userId);
        }

        public Person? GetById(int id)
        {
            return _personInMemoryRepo.GetById(id);
        }

        public Guid GetId()
        {
            return _personInMemoryRepo.GetId();
        }

        public Person? UpdatePerson(Person user)
        {
            return _personInMemoryRepo.UpdatePerson(user);
        }
    }
}