

using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserInMemoryRepository _data;

        public UserService(IUserInMemoryRepository repo)
        {
            _data = repo;
        }

        public IEnumerable<User> GetAllUsers => _data.GetAllUsers;

        public User? AddUser(User user)
        {
            _data.AddUser(user);
            return user;
        }

        public User? DeleteUser(int userId)
        {
            return _data.DeleteUser(userId);
        }

        public User? GetById(int id)
        {
            return _data.GetById(id);
        }

        public Guid GetId()
        {
            return _data.GetId();
        }

        public User? UpdateUser(User user)
        {
            return _data.UpdateUser(user);
        }
    }
}