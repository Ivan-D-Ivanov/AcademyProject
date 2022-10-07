using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public int id { get; init; }

        public DeleteAuthorCommand(int id)
        {
            this.id = id;
        }
    }
}
