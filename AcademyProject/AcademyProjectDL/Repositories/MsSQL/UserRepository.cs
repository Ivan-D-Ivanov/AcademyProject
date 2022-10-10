using System.Data.SqlClient;
using AcademyProjectModels.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AcademyProjectDL.Repositories.MsSQL
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger, IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserInfo> AddUser(UserInfo user)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = (await connection
                        .QueryAsync<UserInfo>
                        ($"INSERT INTO UserInfo (DisplayName,UserName,Email,Password,CreatedDate) VALUES(@DisplayName,@UserName,@Email,@Password,@CreatedDate)", user))
                        .SingleOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddUser)} : {e}");
            }

            return null;
        }

        public async Task<UserInfo> DeleteUser(int userId)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    return (await connection.QueryAsync<UserInfo>($"DELETE FROM UserInfo WHERE UserId = @Id", new { Id = userId })).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(DeleteUser)} : {e}");
            }

            return null;
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsers()
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM UserInfo WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var users = await connection.QueryAsync<UserInfo>(query);
                    return users;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllUsers)} : {e}");
            }

            return Enumerable.Empty<UserInfo>();
        }

        public async Task<UserInfo> GetUserById(int id)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var author = await connection.QueryFirstOrDefaultAsync<UserInfo>($"SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserInfo.UserId = @UserId", new { UserId = id });
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetUserById)} : {e}");
            }

            return null;
        }

        public async Task<UserInfo> GetUserByName(string name)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var user = await connection.QueryFirstOrDefaultAsync<UserInfo>($"SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserInfo.UserName LIKE @UserName", new { UserName = name });
                    return user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetUserByName)} : {e}");
            }

            return null;
        }

        public async Task<UserInfo> UpdateUser(UserInfo user)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<UserInfo>
                        ($"UPDATE UserInfo SET UserId=@UserId,DisplayName=@DisplayName,UserName=@UserName,Email=@Email,Password=@Password,CreatedDate=@CreatedDate WHERE UserId = @UserId", user))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(UpdateUser)} : {e}");
            }

            return null;
        }

        public async Task<UserInfo> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                        var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email=@Email AND Password=@Password",
                            new { Email = email, Password = password });
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
