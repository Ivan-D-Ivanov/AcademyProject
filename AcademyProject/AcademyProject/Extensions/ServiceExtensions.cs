using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectSL.Interfaces;
using AcademyProjectSL.Services;

namespace AcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositoriesPerson(this IServiceCollection services)
        {
            services.AddSingleton<IPersonInMemoryRepository, PersonInMemoryRepository>();
            services.AddSingleton<IAuthorInMemoryRepo, AuthorInMemoryRepo>();
            services.AddSingleton<IBookInMemoryRepo, BookInMemoryRepo>();
            return services;
        }

        public static IServiceCollection RegisterServicePerson(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IAuthorService, AuthorService>();
            return services;
        }
    }
}
