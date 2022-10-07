using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class GetAuthorByIdCommand : IRequest<Author>
    {
        public int _id { get; set; }
        public GetAuthorByIdCommand(int id)
        {
            _id = id;
        }
    }
}
