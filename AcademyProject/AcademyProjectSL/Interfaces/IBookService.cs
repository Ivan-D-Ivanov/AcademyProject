using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks { get; }
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        Book? GetById(int id);
        Book? UpdateBook(Book book);
        Guid GetId();
    }
}
