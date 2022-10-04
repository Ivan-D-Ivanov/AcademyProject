using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AcademyProjectModels;
using AcademyProjectDL.Repositories.InMemoryRepo;
using Dapper;

namespace AcademyProjectDL.Repositories.MsSQL
{
    public class AuthorRepository : IAuthorInMemoryRepo
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;

        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM Authors WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var authors = await connection.QueryAsync<Author>(query);
                    return authors;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAuthors)} : {e}");
            }

            return Enumerable.Empty<Author>();
        }

        public async Task<Author> GetById(int id)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var author = await connection.QueryFirstOrDefaultAsync<Author>($"SELECT * FROM Authors WITH(NOLOCK) WHERE Authors.Id = @Id", new { Id = id });
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }
        public async Task<Author> GetAuthorByName(string name)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var author = await connection.QueryFirstOrDefaultAsync<Author>($"SELECT * FROM Authors WITH(NOLOCK) WHERE Authors.Name LIKE @Name", new { Name = name });
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Author>
                        ($"INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES(@Name,@Age,@DateOfBirth,@NickName)", author))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Author> DeleteAuthor(int authorId)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    return (await connection.QueryAsync<Author>($"DELETE FROM Authors WHERE Id = @Id", new { Id = authorId })).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Author>
                        ($"UPDATE Authors SET Name=@Name,Age=@Age,DateOfBirth=@DateOfBirth,NickName=@NickName WHERE Id = @Id", author))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetById)} : {e}");
            }

            return null;
        }
    }
}
