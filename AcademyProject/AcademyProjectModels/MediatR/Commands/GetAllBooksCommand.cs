using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record GetAllBooksCommand : IRequest<IEnumerable<Book>>
    {
    }
}
