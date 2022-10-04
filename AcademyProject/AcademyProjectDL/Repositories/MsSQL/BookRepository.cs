using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace AcademyProjectDL.Repositories.MsSQL
{
    public class BookRepository : IBookInMemoryRepo
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;

        public BookRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task<Book> AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM Books WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var authors = await connection.QueryAsync<Book>(query);
                    return authors;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)} : {e}");
            }

            return Enumerable.Empty<Book>();
        }

        public Task<Book> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookByAuthorId(int authorId)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var author = await connection.QueryFirstOrDefaultAsync<Book>($"SELECT * FROM Books WITH(NOLOCK) WHERE Books.Id = @Id", new { Id = authorId });
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetBookByAuthorId)} : {e}");
            }

            return null;
        }
    }
}
