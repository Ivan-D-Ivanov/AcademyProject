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
        private readonly ILogger<BookRepository> _logger;
        private readonly IConfiguration _configuration;

        public BookRepository(ILogger<BookRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Book> AddBook(Book book)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Book>
                        ($"INSERT INTO Books (AuthorId,Title,LastUpdated,Quantity,Price) VALUES(@AuthorId,@Title,@LastUpdated,@Quantity,@Price)", book))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    return (await connection.QueryAsync<Book>($"DELETE FROM Books WHERE Id = @Id", new { Id = bookId })).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
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

        public async Task<Book> GetById(int id)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var author = await connection.QueryFirstOrDefaultAsync<Book>($"SELECT * FROM Books WITH(NOLOCK) WHERE Books.Id = @Id", new { Id = id });
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Book> GetBookByTitle(string title)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var book = await connection.QueryFirstOrDefaultAsync<Book>($"SELECT * FROM Books WITH(NOLOCK) WHERE Books.Title LIKE @Title", new { Title = title });
                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Book>
                        ($"UPDATE Books SET AuthorId=@AuthorId,Title=@Title,LastUpdated=@LastUpdated,Quantity=@Quantity,Price=@Price WHERE Id = @Id", book))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
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
