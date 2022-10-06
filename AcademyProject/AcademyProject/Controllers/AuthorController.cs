using System.Net;
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
            var result = await _authorService.GetById(id);
            if (result == null) return NotFound(id);
            
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _authorService.AddAuthor(authorRequest);
            if (result.HttpStatusCode == HttpStatusCode.BadRequest) return BadRequest(result);
            return Ok(result);
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
            var result = await _authorService.GetAuthorByName(name);
            if(result == null) return BadRequest(name);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteAuthorById))]
        public async Task<IActionResult> DeleteAuthorById(int authorId)
        {
            var result = await _authorService.DeleteAuthor(authorId);
            if (result == null) return NotFound(authorId);
            return Ok(result);
        }
    }
}
