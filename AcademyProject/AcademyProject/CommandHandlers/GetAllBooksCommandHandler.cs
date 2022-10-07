using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class GetAllBooksCommandHandler : IRequestHandler<GetAllBooksCommand, IEnumerable<Book>>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public GetAllBooksCommandHandler(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookInMemoryRepo.GetAllBooks();
        }
    }
}
