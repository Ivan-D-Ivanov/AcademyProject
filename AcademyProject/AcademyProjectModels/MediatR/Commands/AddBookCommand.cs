using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public record AddBookCommand(AddBookRequest book) : IRequest<AddBookResponse>
    {
        public readonly AddBookRequest _book = book;
    }
}
