using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IBookInMemoryRepo
    {
        IEnumerable<Book> GetAllBooks { get; }
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        Book? GetById(int id);
        Book? UpdateBook(Book book);
    }
}
