using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetAllAuthorsCommandHandler : IRequestHandler<GetAllAuthorsCommand, IEnumerable<Author>>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public GetAllAuthorsCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorInMemoryRepo.GetAuthors();
        }
    }
}
