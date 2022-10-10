using AcademyProjectModels.Users;

namespace AcademyProjectSL.Interfaces
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfo>> GetAllUsers();
        Task<UserInfo?> GetUserById(int id);
        Task<UserInfo?> GetUserByName(string title);
        Task<UserInfo?> AddUser(UserInfo user);
        Task<UserInfo?> DeleteUser(int userId);
        Task<UserInfo?> UpdateUser(UserInfo user);
        Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
