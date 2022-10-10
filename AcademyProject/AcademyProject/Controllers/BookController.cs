using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllBooks))]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
            //return Ok(await _bookService.GetAllBooks());
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBookById))]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _mediator.Send(new GetBookByIdCommand(id)));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBookByTitle))]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            return Ok(await _mediator.Send(new GetBookByTitleCommand(title)));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest addBookRequest)
        {
            var result = await _mediator.Send(new AddBookCommand(addBookRequest));
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest updateBookRequest)
        {
            var result = await _mediator.Send(new UpdateBookCommand(updateBookRequest));
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _mediator.Send(new DeleteBookCommand(id)));
        }
    }
}
