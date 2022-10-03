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
        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {
            return Ok(_bookService.GetAllBooks);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            return Ok(_bookService.GetById(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(Add))]
        public IActionResult Add([FromBody] AddBookRequest addBookRequest)
        {
            var result = _bookService.AddBook(addBookRequest);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(Update))]
        public IActionResult Update(UpdateBookRequest updateBookRequest)
        {
            var result = _bookService.UpdateBook(updateBookRequest);
            return Ok(result);
        }
    }
}
