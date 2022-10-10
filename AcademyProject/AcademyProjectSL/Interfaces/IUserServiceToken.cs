using AcademyProjectModels.Users;

namespace AcademyProjectSL.Interfaces
{
    public interface IUserServiceToken
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
