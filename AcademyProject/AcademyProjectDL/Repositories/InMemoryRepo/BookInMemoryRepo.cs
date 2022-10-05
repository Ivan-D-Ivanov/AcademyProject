using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public class BookInMemoryRepo
    {
        private static List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "TTTT",
                AuthorId = 4
            },
            new Book()
            {
                Id = 2,
                Title = "CCCC",
                AuthorId = 3
            },
            new Book()
            {
                Id = 3,
                Title = "FFFF",
                AuthorId = 2
            },
            new Book()
            {
                Id = 4,
                Title = "HHHH",
                AuthorId = 1
            },
        };

        public IEnumerable<Book> GetAllBooks => _books;

        public Book? AddBook(Book book)
        {
            _books.Add(book);
            return book;
        }

        public Book? DeleteBook(int bookId)
        {
            var authorToRemove = _books.FirstOrDefault(x => x.Id == bookId);
            if (authorToRemove == null) return null;
            _books.Remove(authorToRemove);

            return authorToRemove;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public Book? UpdateBook(Book book)
        {
            var existingUser = _books.FirstOrDefault(x => x.Id == book.Id);
            if (existingUser == null) return null;

            _books.Remove(existingUser);
            _books.Add(book);

            return book;
        }
    }
}
