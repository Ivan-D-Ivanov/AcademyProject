using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public class UserInMemoryRepository : IUserInMemoryRepository
    {
        private static List<User> _users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name = "Ivan",
                Age = 29
            },
            new User()
            {
                Id = 2,
                Name = "Pesho",
                Age = 29
            },
            new User()
            {
                Id = 3,
                Name = "Georgi",
                Age = 29
            },
            new User()
            {
                Id = 4,
                Name = "Dimitar",
                Age = 29
            },
        };

        public Guid Id { get; set; }

        public UserInMemoryRepository()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<User> GetAllUsers => _users;

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public User? AddUser(User user)
        {
            _users.Add(user);
            return user;
        }

        public User? UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null) return null;

            _users.Remove(existingUser);
            _users.Add(user);

            return user;
        }

        public User? DeleteUser(int userId)
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