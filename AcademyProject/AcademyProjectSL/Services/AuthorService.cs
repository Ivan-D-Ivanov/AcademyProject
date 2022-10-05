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
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public AuthorService(IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper, IBookInMemoryRepo bookInMemoryRepo)
        {
            _mapper = mapper;
            _authorInMemoryRepo = authorInMemoryRepo;
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<IEnumerable<Author>> GetAuthors() => await _authorInMemoryRepo.GetAuthors();

        public async Task<Author?> GetById(int id)
        {
            if (id <= 0) return null;
            return await _authorInMemoryRepo.GetById(id);
        }

        public async Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest)
        {
            if (authorRequest == null) return new AddAuthorResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = await _authorInMemoryRepo.GetAuthorByName(authorRequest.Name);

            if (result != null) return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The Author already exists" };

            var author = _mapper.Map<Author>(authorRequest);
            var result2 = await _authorInMemoryRepo.AddAuthor(author);
            return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }

        public async Task<UpdateAuthoreResponse> UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = await _authorInMemoryRepo.GetById(authorRequest.Id);

            if (result == null) return new UpdateAuthoreResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Cant find this Author: {authorRequest.Name}" };

            var result2 = _mapper.Map<Author>(authorRequest);
            await _authorInMemoryRepo.UpdateAuthor(result2);
            return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }

        public async Task<Author?> DeleteAuthor(int authorId)
        {
            if (authorId <= 0) return null;
            var author = await GetById(authorId);
            if (author == null) return null;
            if (await _bookInMemoryRepo.GetBookByAuthorId(author.Id) != null) return null;
            return await _authorInMemoryRepo.DeleteAuthor(authorId);
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _authorInMemoryRepo.GetAuthorByName(name);
        }
    }
}
