using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllEmployees))]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _employeeService.GetAllEmployees());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetEmployeeById))]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return Ok(await _employeeService.GetEmployeeById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetEmployeeByName))]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            return Ok(await _employeeService.GetEmployeeByName(name));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddEmployee))]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return Ok(await _employeeService.DeleteEmployee(id));
        }
    }
}
