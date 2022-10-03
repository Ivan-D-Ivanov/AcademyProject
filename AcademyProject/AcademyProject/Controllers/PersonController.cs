using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService uService)
        {
            _logger = logger;
            _personService = uService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {
            return Ok(_personService.GetAllPersons);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            return Ok(_personService.GetById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(Add))]
        public IActionResult Add([FromBody] Person user)
        {
            return Ok(_personService.AddPerson(user));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(Update))]
        public IActionResult Update(Person user)
        {
            return Ok(_personService.UpdatePerson(user));
        }
    }
}
