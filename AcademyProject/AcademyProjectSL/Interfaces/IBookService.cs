using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetById(int id);
        Task<Book?> GetBookByAuthorId(int id);
        Task<Book?> GetBookByTitle(string title);
        Task<Book?> DeleteBook(int bookId);
        Task<AddBookResponse> AddBook(AddBookRequest bookRequest);
        Task<UpdateBookResponse> UpdateBook(UpdateBookRequest bookRequest);
    }
}
