using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public class PersonInMemoryRepository : IPersonInMemoryRepository
    {
        private static List<Person> _users = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "Ivan",
                Age = 29
            },
            new Person()
            {
                Id = 2,
                Name = "Pesho",
                Age = 29
            },
            new Person()
            {
                Id = 3,
                Name = "Georgi",
                Age = 29
            },
            new Person()
            {
                Id = 4,
                Name = "Dimitar",
                Age = 29
            },
        };

        public Guid Id { get; set; }

        public PersonInMemoryRepository()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<Person> GetAllPersons => _users;

        public Person? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddPerson(Person user)
        {
            _users.Add(user);
            return user;
        }

        public Person? UpdatePerson(Person user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null) return null;

            _users.Remove(existingUser);
            _users.Add(user);

            return user;
        }

        public Person? DeletePerson(int userId)
        {
            var userToRemove = _users.FirstOrDefault(x => x.Id == userId);
            if(userToRemove == null) return null;
            _users.Remove(userToRemove);

            return userToRemove;
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}