using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record GetBookByTitleCommand(string title) : IRequest<Book>
    {
    }
}
