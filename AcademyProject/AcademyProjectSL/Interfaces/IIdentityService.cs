using AcademyProjectModels.Users;
using Microsoft.AspNetCore.Identity;

namespace AcademyProjectSL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);

        Task<UserInfo?> CheckUserAndPass(string userName, string password);

        Task<IEnumerable<string>> GetUserRole(UserInfo user);
    }
}
