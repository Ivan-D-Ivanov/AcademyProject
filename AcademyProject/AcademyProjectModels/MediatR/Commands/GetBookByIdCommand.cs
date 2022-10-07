using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record GetBookByIdCommand(int id) : IRequest<Book>
    {
    }
}
