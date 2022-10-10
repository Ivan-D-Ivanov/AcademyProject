using System.Data.SqlClient;
using AcademyProjectModels.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AcademyProjectDL.Repositories.MsSQL
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserInfoStore(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await _userRepository.AddUser(user);
            var result = _userRepository.GetUserByName(user.UserName.ToUpper());

            if (result == null)
            {
                return IdentityResult.Failed();
            }
            else
            {
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteUser(user.UserId);
            if (result == null)
            {
                return IdentityResult.Failed();
            }
            else
            {
                return IdentityResult.Success;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserById(int.Parse(userId));
            return result;
        }

        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByName(normalizedUserName);
            return result;
        }

        public async Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnction"));
            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WITH(NOLOCK) WHERE UserId = @userId", new { user.UserId });
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync<string>(
                    "SELECT r.RoleName as Name FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId)", new { user.UserId });
                return result.ToList();
            }
        }

        public Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(user.UserName);
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnction"));
            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordHash WHERE UserId = @userId", new { user.UserId });
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.UpdateUser(user);
            if (result == null)
            {
                return IdentityResult.Failed();
            }
            else
            {
                return IdentityResult.Success;
            }
        }
    }
}
