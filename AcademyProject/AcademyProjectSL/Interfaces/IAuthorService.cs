using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;

namespace AcademyProjectSL.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest);
        Task<Author?> DeleteAuthor(int userId);
        Task<Author?> GetById(int id);
        Task<UpdateAuthoreResponse> UpdateAuthor(UpdateAuthorRequest authorRequest);
        Task<Author?> GetAuthorByName(string name);
    }
}
