using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers { get; }

        User? AddUser(User user);
        User? DeleteUser(int userId);
        User? GetById(int id);
        User? UpdateUser(User user);
        Guid GetId();
    }
}
