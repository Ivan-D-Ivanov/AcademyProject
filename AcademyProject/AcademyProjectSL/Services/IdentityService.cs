using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AcademyProjectSL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;
        private readonly RoleManager<UserRole> _userRole;

        public IdentityService(UserManager<UserInfo> userManager, IPasswordHasher<UserInfo> passwordHasher, RoleManager<UserRole> userRole)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _userRole = userRole;
        }
        public async Task<UserInfo> CheckUserAndPass(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null) return null;

            //if (user.UserName == userName && user.Password == password) return user;
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user)
        {
            var existingUser = await _userManager.GetUserIdAsync(user);

            if (!string.IsNullOrEmpty(existingUser)) return await _userManager.CreateAsync(user);

            return IdentityResult.Failed();
        }

        public async Task<IEnumerable<string>> GetUserRole(UserInfo user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
