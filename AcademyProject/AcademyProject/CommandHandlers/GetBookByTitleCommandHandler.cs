using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetBookByTitleCommandHandler : IRequestHandler<GetBookByTitleCommand, Book>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public GetBookByTitleCommandHandler(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<Book?> Handle(GetBookByTitleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.title)) return null;
            var result = await _bookInMemoryRepo.GetBookByTitle(request.title);
            if (result == null) return null;
            return await _bookInMemoryRepo.GetBookByTitle(request.title);
        }
    }
}
