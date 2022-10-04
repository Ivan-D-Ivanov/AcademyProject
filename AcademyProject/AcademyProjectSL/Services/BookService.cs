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

        public async Task<IEnumerable<Book>> GetAllBooks() => await _bookInMemoryRepo.GetAllBooks();

        public async Task<Book?> GetById(int id)
        {
            if(id <= 0) return null;
            return await _bookInMemoryRepo.GetById(id);
        }

        public async Task<Book?> GetBookByAuthorId(int id)
        {
            if (id <= 0) return null;
            return await _bookInMemoryRepo.GetBookByAuthorId(id);
        }

        public async Task<AddBookResponse> AddBook(AddBookRequest bookRequest)
        {
            if (bookRequest == null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _bookInMemoryRepo.GetById(bookRequest.Id);

            if (result != null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This Book already exists" };

            var resultFromMap = _mapper.Map<Book>(bookRequest);
            await _bookInMemoryRepo.AddBook(resultFromMap);
            return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }

        public async Task<Book?> DeleteBook(int bookId)
        {
            return await _bookInMemoryRepo.DeleteBook(bookId);
        }

        public async Task<UpdateBookResponse> UpdateBook(UpdateBookRequest bookRequest)
        {
            if (bookRequest == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _bookInMemoryRepo.GetById(bookRequest.Id);
            if (result == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Book" };

            var resultFromMap = _mapper.Map<Book>(bookRequest);
            await _bookInMemoryRepo.UpdateBook(resultFromMap);
            return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }
    }
}
