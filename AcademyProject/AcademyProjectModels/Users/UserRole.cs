using Microsoft.AspNetCore.Identity;

namespace AcademyProjectModels.Users
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }
    }
}
