using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IUserInMemoryRepository
    {
        IEnumerable<User> GetAllUsers { get; }

        User? AddUser(User user);
        User? DeleteUser(int userId);
        User? GetById(int id);
        User? UpdateUser(User user);
        Guid GetId();
    }
}