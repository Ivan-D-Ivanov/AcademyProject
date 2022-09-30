using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllUsers { get; }

        Author? AddAuthor(Author user);
        Author? DeleteAuthor(int userId);
        Author? GetById(int id);
        Author? UpdateAuthor(Author user);
        Guid GetId();
    }
}
