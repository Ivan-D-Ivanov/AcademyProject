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
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public BookService(IBookInMemoryRepo bookInMemoryRepo, IMapper mapper, IAuthorInMemoryRepo authorInMemoryRepo)
        {
            _mapper = mapper;
            _bookInMemoryRepo = bookInMemoryRepo;
            _authorInMemoryRepo = authorInMemoryRepo;
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

        public async Task<Book?> GetBookByTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;
            var result = await _bookInMemoryRepo.GetBookByTitle(title);
            if (result == null) return null;
            return result;
        }

        public async Task<AddBookResponse> AddBook(AddBookRequest bookRequest)
        {
            if (bookRequest == null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _bookInMemoryRepo.GetBookByTitle(bookRequest.Title);
            if (result != null) return new AddBookResponse() 
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This Book already exists" };

            var authorExist = await _authorInMemoryRepo.GetById(bookRequest.AuthorId);
            if (authorExist == null) return new AddBookResponse() 
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Author with this ID : {bookRequest.AuthorId} does not exist" };
            
            var resultFromMap = _mapper.Map<Book>(bookRequest);
            resultFromMap.LastUpdated = DateTime.UtcNow;
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

            var authorExist = await _authorInMemoryRepo.GetById(bookRequest.AuthorId);
            if (authorExist == null) return new UpdateBookResponse()
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Author with this ID : {bookRequest.AuthorId} does not exist" };

            var resultFromMap = _mapper.Map<Book>(bookRequest);
            resultFromMap.LastUpdated = DateTime.UtcNow;
            await _bookInMemoryRepo.UpdateBook(resultFromMap);
            return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }
    }
}
