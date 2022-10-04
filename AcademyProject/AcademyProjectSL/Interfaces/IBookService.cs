using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<AddBookResponse> AddBook(AddBookRequest bookRequest);
        Task<Book?> DeleteBook(int bookId);
        Task<Book?> GetById(int id);
        Task<Book?> GetBookByAuthorId(int id);
        Task<UpdateBookResponse> UpdateBook(UpdateBookRequest bookRequest);
    }
}
