using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record DeleteBookCommand(int id) : IRequest<Book>
    {
    }
}
