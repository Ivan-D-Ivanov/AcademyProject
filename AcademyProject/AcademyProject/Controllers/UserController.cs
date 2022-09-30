

using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService uService)
        {
            _logger = logger;
            _userService = uService;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<User> Get()
        {
            return _userService.GetAllUsers;
        }

        [HttpGet(nameof(GetById))]
        public User? GetById(int id)
        {
            return _userService.GetById(id);
        }

        [HttpPost(nameof(Add))]
        public User? Add([FromBody] User user)
        {
            return _userService.AddUser(user);
        }

        [HttpPost(nameof(Update))]
        public User? Update(User user)
        {
            return _userService.UpdateUser(user);
        }
    }
}
