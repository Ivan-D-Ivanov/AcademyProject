using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public interface IAuthorInMemoryRepo
    {
        IEnumerable<Author> GetAuthors { get; }
        Author? AddAuthor(Author user);
        Author? DeleteAuthor(int userId);
        Author? GetById(int id);
        Author? UpdateAuthor(Author user);
        Author? GetAuthorByName(string name);
    }
}
