using AcademyProjectModels.Request;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public IActionResult GetAllAuthors()
        {
            try
            {
                throw new NullReferenceException();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return Ok(_authorService.GetAuthors);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var result = _authorService.GetById(id);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public IActionResult AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = _authorService.AddAuthor(authorRequest);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]
        public IActionResult UpdateAuthor(UpdateAuthorRequest updateAuthorRequest)
        {
            var result = _authorService.UpdateAuthor(updateAuthorRequest);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAuthorByName))]
        public IActionResult GetAuthorByName(string name)
        {
            return Ok(_authorService.GetAuthorByName(name));
        }
    }
}
