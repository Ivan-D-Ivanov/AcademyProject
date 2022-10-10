using AcademyProjectDL.Repositories.MsSQL;
using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserRepository _userRepository;

        public UserInfoService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserInfo>> GetAllUsers() => await _userRepository.GetAllUsers();

        public async Task<UserInfo> AddUser(UserInfo user)
        {
            if (user == null) return null;

            var authorExist = await _userRepository.GetUserById(user.UserId);
            if (authorExist != null) return null;

            return await _userRepository.AddUser(user);
        }

        public async Task<UserInfo> DeleteUser(int userId)
        {
            if (userId <= 0) return null;
            return await _userRepository.DeleteUser(userId);
        }


        public async Task<UserInfo> GetUserById(int id)
        {
            if (id <= 0) return null;
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserInfo> GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var result = await _userRepository.GetUserByName(name);
            if (result == null) return null;
            return result;
        }

        public async Task<UserInfo> UpdateUser(UserInfo user)
        {
            if (user == null) return null;

            var authorExist = await _userRepository.GetUserByName(user.UserName);
            if (authorExist == null) return null;

            return await _userRepository.UpdateUser(user);
        }

        public Task<UserInfo> GetUserInfoAsync(string email, string password)
        {
            return _userRepository.GetUserInfoAsync(email, password);
        }
    }
}
