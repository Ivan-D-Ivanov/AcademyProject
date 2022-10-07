using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using MediatR;

namespace AcademyProjectModels.MediatR.Commands
{
    public class AddAuthorCommand : IRequest<AddAuthorResponse>
    {
        public AddAuthorRequest _author { get; set; }

        public AddAuthorCommand(AddAuthorRequest authorRequest)
        {
            _author = authorRequest;
        }
    }
}
