using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetAuthorByNameCommandHandler : IRequestHandler<GetAuthorByNameCommand, Author>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public GetAuthorByNameCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
        }

        public async Task<Author?> Handle(GetAuthorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorInMemoryRepo.GetAuthorByName(request._name);
        }
    }
}
