using AcademyProjectModels;
using AcademyProjectModels.Request;
using AutoMapper;

namespace AcademyProject.AutoMapping
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();
        }
    }
}
