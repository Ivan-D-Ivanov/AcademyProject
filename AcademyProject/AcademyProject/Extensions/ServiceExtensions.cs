﻿using AcademyProjectCaches.CacheInMemoryCollection;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectDL.Repositories.MsSQL;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
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
            return services;
        }

        public static IServiceCollection RegisterServicePerson(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<IKafkaPublisherService<int, Book>, KafkaPublisherService<int, Book>>();
            return services;
        }
    }
}
