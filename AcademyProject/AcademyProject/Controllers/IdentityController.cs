using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AcademyProjectModels.Request;
using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;

        public IdentityController(IConfiguration configuration, IIdentityService identityService)
        {
            _configuration = configuration;
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(CreateUserAsync))]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserInfo user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest($"Username or password is missing");
            }

            var result = await _identityService.CreateAsync(user);

            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest != null && !string.IsNullOrEmpty(loginRequest.UserName) && !string.IsNullOrEmpty(loginRequest.Password))
            {
                var user = await _identityService.CheckUserAndPass(loginRequest.UserName, loginRequest.Password);

                if (user != null)
                {
                    var userRole = await _identityService.GetUserRole(user);

                    var claims = new List<Claim>();
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value);
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
                        new Claim("UserId", user.UserId.ToString());
                        new Claim("DisplayName", user.DisplayName ?? string.Empty);
                        new Claim("UserName", user.UserName ?? string.Empty);
                        new Claim("Email", user.Email ?? string.Empty);
                    };

                    foreach (var role in userRole)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials. Username or Password does not match");
                }
            }
            else
            {
                return BadRequest("Missing Username and/or password");
            }
        }
    }
}
