using System.Net;
using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Request;
using AcademyProjectSL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdCommand(id));
            if (result == null) return NotFound(id);
            
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _mediator.Send(new AddAuthorCommand(authorRequest));
            if (result.HttpStatusCode == HttpStatusCode.BadRequest) return BadRequest(result);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateAuthor))]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest updateAuthorRequest)
        {
            return Ok(await _mediator.Send(new UpdateAuthorCommand(updateAuthorRequest)));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAuthorByName))]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            var result = await _mediator.Send(new GetAuthorByNameCommand(name));
            if(result == null) return BadRequest(name);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteAuthorById))]
        public async Task<IActionResult> DeleteAuthorById(int authorId)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(authorId));
            if (result == null) return NotFound(authorId);
            return Ok(result);
        }
    }
}
