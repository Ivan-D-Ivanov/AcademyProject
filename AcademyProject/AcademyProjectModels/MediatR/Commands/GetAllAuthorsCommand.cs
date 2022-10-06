using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class GetAllAuthorsCommand : IRequest<IEnumerable<Author>>
    {
    }
}
