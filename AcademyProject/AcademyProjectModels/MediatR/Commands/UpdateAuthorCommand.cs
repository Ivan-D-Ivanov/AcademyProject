using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class UpdateAuthorCommand : IRequest<UpdateAuthoreResponse>
    {
        public UpdateAuthorRequest _authorRequest { get; set; }

        public UpdateAuthorCommand(UpdateAuthorRequest authorRequest)
        {
            _authorRequest = authorRequest;
        }
    }
}
