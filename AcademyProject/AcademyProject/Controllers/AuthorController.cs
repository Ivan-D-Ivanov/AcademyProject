using AcademyProjectSL.Interfaces;
using AcademyProjectModels;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(ILogger<PersonController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return _authorService.GetId();
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Author> Get()
        {
            return _authorService.GetAuthors;
        }

        [HttpGet(nameof(GetById))]
        public Author GetById(int id)
        {
            var result = _authorService.GetById(id);
            return result;
        }

        [HttpPost(nameof(Add))]
        public Author? Add([FromBody] Author author)
        {
            return _authorService.AddAuthor(author);
        }
    }
}
