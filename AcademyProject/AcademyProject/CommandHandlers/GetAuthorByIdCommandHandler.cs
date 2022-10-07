using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetAuthorByIdCommandHandler : IRequestHandler<GetAuthorByIdCommand, Author>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public GetAuthorByIdCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
        }

        public async Task<Author?> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request._id <= 0) return null;
            return await _authorInMemoryRepo.GetById(request._id);
        }
    }
}
