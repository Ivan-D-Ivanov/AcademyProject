using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthors { get; }
        AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest);
        Author? DeleteAuthor(int userId);
        Author? GetById(int id);
        UpdateAuthoreResponse UpdateAuthor(UpdateAuthorRequest authorRequest);
        Author? GetAuthorByName(string name);
    }
}
