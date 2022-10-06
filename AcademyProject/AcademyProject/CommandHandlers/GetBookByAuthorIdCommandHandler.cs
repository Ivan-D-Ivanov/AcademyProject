using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetBookByAuthorIdCommandHandler : IRequestHandler<GetBookByAuthorIdCommand, Book>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public GetBookByAuthorIdCommandHandler(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<Book?> Handle(GetBookByAuthorIdCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0) return null;
            return await _bookInMemoryRepo.GetBookByAuthorId(request.id);
        }
    }
}
