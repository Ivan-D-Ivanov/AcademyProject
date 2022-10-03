using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using AcademyProjectSL.Interfaces;
using AutoMapper;

namespace AcademyProjectSL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public AuthorService(IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _authorInMemoryRepo = authorInMemoryRepo;
        }

        public IEnumerable<Author> GetAuthors => _authorInMemoryRepo.GetAuthors;

        public Author? GetById(int id)
        {
            return _authorInMemoryRepo.GetById(id);
        }

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest)
        {
            if (authorRequest == null) return new AddAuthorResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = _authorInMemoryRepo.GetAuthorByName(authorRequest.Name);

            if (result != null) return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The Author already exists" };

            var author = _mapper.Map<Author>(authorRequest);
            var result2 = _authorInMemoryRepo.AddAuthor(author);
            return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }

        public UpdateAuthoreResponse UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = _authorInMemoryRepo.GetById(authorRequest.Id);

            if (result == null) return new UpdateAuthoreResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Cant find this Author: {authorRequest.Name}" };

            var result2 = _mapper.Map<Author>(authorRequest);
            _authorInMemoryRepo.UpdateAuthor(result2);
            return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }

        public Author? DeleteAuthor(int authorId)
        {
            return _authorInMemoryRepo.DeleteAuthor(authorId);
        }

        public Author? GetAuthorByName(string name)
        {
            return _authorInMemoryRepo.GetAuthorByName(name);
        }
    }
}
