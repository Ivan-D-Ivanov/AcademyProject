using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IBookInMemoryRepo
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> AddBook(Book book);
        Task<Book?> DeleteBook(int bookId);
        Task<Book?> GetById(int id);
        Task<Book?> UpdateBook(Book book);
        Task<Book?> GetBookByAuthorId(int id);
        Task<Book?> GetBookByTitle(string title);
    }
}
