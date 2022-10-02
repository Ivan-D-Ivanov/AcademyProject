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

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllPersons;
        }

        [HttpGet(nameof(GetById))]
        public Person? GetById(int id)
        {
            return _personService.GetById(id);
        }

        [HttpPost(nameof(Add))]
        public Person? Add([FromBody] Person user)
        {
            return _personService.AddPerson(user);
        }

        [HttpPost(nameof(Update))]
        public Person? Update(Person user)
        {
            return _personService.UpdatePerson(user);
        }
    }
}
