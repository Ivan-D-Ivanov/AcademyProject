using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Response;
using AutoMapper;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;
        private readonly IMapper _mapper;

        public AddAuthorCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request._author == null) return new AddAuthorResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = await _authorInMemoryRepo.GetAuthorByName(request._author.Name);

            if (result != null) return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The Author already exists", Author = result };

            var author = _mapper.Map<Author>(request._author);
            var result2 = await _authorInMemoryRepo.AddAuthor(author);
            return new AddAuthorResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }
    }
}
