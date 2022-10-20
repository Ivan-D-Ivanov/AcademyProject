using AcademyProjectCaches.CacheInMemoryCollection;
using AcademyProjectDL.DLInterfaces;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectDL.Repositories.Mongo;
using AcademyProjectDL.Repositories.MsSQL;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using AcademyProjectSL.ServiceProviders;
using AcademyProjectSL.Services;

namespace AcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositoriesPerson(this IServiceCollection services)
        {
            services.AddSingleton<IPersonInMemoryRepository, PersonInMemoryRepository>();
            services.AddSingleton<IAuthorInMemoryRepo, AuthorRepository>();
            services.AddSingleton<IBookInMemoryRepo, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<GenericCollection<Book>>();
            services.AddSingleton<IPurcahaseRepository, PurchaseRepository>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            return services;
        }

        public static IServiceCollection RegisterServicePerson(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<IKafkaPublisherService<int, Book>, KafkaPublisherService<int, Book>>();
            services.AddSingleton<IPurchaseService, PurchaseService>();
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();
            services.AddSingleton<PurchaseServiceProvider>();
            return services;
        }
    }
}
