using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<PersonController> logger, IBookService bService)
        {
            _logger = logger;
            _bookService = bService;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAllBooks;
        }

        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            return _bookService.GetById(id);
        }

        [HttpPost(nameof(Add))]
        public Book? Add([FromBody] Book book)
        {
            return _bookService.AddBook(book);
        }

        [HttpPost(nameof(Update))]
        public Book? Update(Book book)
        {
            return _bookService.UpdateBook(book);
        }
    }
}
