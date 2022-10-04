using AcademyProjectModels.Request;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bService)
        {
            _logger = logger;
            _bookService = bService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllBooks))]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBookById))]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _bookService.GetById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest addBookRequest)
        {
            var result = await _bookService.AddBook(addBookRequest);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest updateBookRequest)
        {
            var result = await _bookService.UpdateBook(updateBookRequest);
            return Ok(result);
        }
    }
}
