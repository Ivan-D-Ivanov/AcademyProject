using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks { get; }
        AddBookResponse AddBook(AddBookRequest bookRequest);
        Book? DeleteBook(int bookId);
        Book? GetById(int id);
        UpdateBookResponse UpdateBook(UpdateBookRequest bookRequest);
    }
}
