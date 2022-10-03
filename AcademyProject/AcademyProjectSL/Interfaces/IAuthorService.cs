using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthors { get; }
        Author? AddAuthor(Author user);
        Author? DeleteAuthor(int userId);
        Author? GetById(int id);
        Author? UpdateAuthor(Author user);
        Guid GetId();
    }
}
