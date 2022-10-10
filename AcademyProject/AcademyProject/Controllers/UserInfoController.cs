using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserInfoService _userService;

        public UserInfoController(ILogger<UserInfoController> logger, IUserInfoService user)
        {
            _logger = logger;
            _userService = user;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllUsers))]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetUserByName))]
        public async Task<IActionResult> GetUserByName(string name)
        {
            return Ok(await _userService.GetUserByName(name));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddUser))]
        public async Task<IActionResult> AddUser([FromBody] UserInfo user)
        {
            var result = await _userService.AddUser(user);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(UserInfo user)
        {
            var result = await _userService.UpdateUser(user);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _userService.DeleteUser(id));
        }
    }
}
