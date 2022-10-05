using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author?> GetById(int id);
        Task<Author?> GetAuthorByName(string name);
        Task<Author?> DeleteAuthor(int userId);
        Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest);
        Task<UpdateAuthoreResponse> UpdateAuthor(UpdateAuthorRequest authorRequest);
    }
}
