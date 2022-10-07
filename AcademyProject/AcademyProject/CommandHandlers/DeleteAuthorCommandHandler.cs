using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public DeleteAuthorCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo, IBookInMemoryRepo bookInMemoryRepo)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<Author?> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0) return null;
            var author = await _authorInMemoryRepo.GetById(request.id);
            if (author == null) return null;
            if (await _bookInMemoryRepo.GetBookByAuthorId(author.Id) != null) return null;
            return await _authorInMemoryRepo.DeleteAuthor(request.id);
        }
    }
}
