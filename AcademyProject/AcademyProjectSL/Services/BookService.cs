using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using AcademyProjectSL.Interfaces;
using AutoMapper;

namespace AcademyProjectSL.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public BookService(IBookInMemoryRepo bookInMemoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public IEnumerable<Book> GetAllBooks => _bookInMemoryRepo.GetAllBooks;

        public AddBookResponse AddBook(AddBookRequest bookRequest)
        {
            if (bookRequest == null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = _bookInMemoryRepo.GetAllBooks.FirstOrDefault(b => b.Id == bookRequest.Id);

            if (result != null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This Book already exists" };

            var resultFromMap = _mapper.Map<Book>(bookRequest);
            _bookInMemoryRepo.AddBook(resultFromMap);
            return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }

        public Book? DeleteBook(int bookId)
        {
            return _bookInMemoryRepo.DeleteBook(bookId);
        }

        public Book? GetById(int id)
        {
            return _bookInMemoryRepo.GetById(id);
        }

        public UpdateBookResponse UpdateBook(UpdateBookRequest bookRequest)
        {
            if (bookRequest == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = _bookInMemoryRepo.GetAllBooks.FirstOrDefault(b => b.Id == bookRequest.Id);
            if (result == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Book" };

            var resultFromMap = _mapper.Map<Book>(bookRequest);
            _bookInMemoryRepo.UpdateBook(resultFromMap);
            return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }
    }
}
