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
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authorService.GetAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _authorService.GetById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            return Ok(await _authorService.AddAuthor(authorRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateAuthor))]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest updateAuthorRequest)
        {
            return Ok(await _authorService.UpdateAuthor(updateAuthorRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAuthorByName))]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            return Ok(await _authorService.GetAuthorByName(name));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteAuthorById))]
        public async Task<IActionResult> DeleteAuthorById(int authorId)
        {
            return Ok(await _authorService.DeleteAuthor(authorId));
        }
    }
}
