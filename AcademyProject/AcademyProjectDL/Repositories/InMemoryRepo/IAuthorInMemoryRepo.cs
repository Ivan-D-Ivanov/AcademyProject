using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IAuthorInMemoryRepo
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author?> AddAuthor(Author user);
        Task<Author?> DeleteAuthor(int userId);
        Task<Author?> GetById(int id);
        Task<Author?> UpdateAuthor(Author user);
        Task<Author?> GetAuthorByName(string name);
    }
}
