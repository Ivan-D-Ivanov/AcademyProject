using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class GetAuthorByNameCommand : IRequest<Author>
    {
        public string _name { get; set; }

        public GetAuthorByNameCommand(string name)
        {
            _name = name;
        }
    }
}
