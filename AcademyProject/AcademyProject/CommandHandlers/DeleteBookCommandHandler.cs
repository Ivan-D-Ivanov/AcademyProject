using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public DeleteBookCommandHandler(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public async Task<Book?> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookInMemoryRepo.DeleteBook(request.id); 
        }
    }
}
