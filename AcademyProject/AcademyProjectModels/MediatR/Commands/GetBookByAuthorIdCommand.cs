using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record GetBookByAuthorIdCommand(int id) : IRequest<Book>
    {
    }
}
