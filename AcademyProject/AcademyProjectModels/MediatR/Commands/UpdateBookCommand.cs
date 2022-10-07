using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record UpdateBookCommand(UpdateBookRequest book) : IRequest<UpdateBookResponse>
    {
        public readonly UpdateBookRequest _book = book;
    }
}
