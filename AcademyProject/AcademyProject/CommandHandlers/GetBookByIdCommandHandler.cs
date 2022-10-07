using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetBookByIdCommandHandler : IRequestHandler<GetBookByIdCommand, Book>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public GetBookByIdCommandHandler(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }   

        public async Task<Book?> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0) return null;
            return await _bookInMemoryRepo.GetById(request.id);
        }
    }
}
