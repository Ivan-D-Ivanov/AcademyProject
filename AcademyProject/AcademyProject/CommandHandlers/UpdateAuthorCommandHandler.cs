using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Response;
using AutoMapper;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthoreResponse>
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
            _mapper = mapper;
        }

        public async Task<UpdateAuthoreResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request._authorRequest == null) return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Request is null" };

            var result = await _authorInMemoryRepo.GetAuthorByName(request._authorRequest.Name);

            if (result == null) return new UpdateAuthoreResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Cant find this Author: {request._authorRequest.Name}" };

            var result2 = _mapper.Map<Author>(request._authorRequest);
            await _authorInMemoryRepo.UpdateAuthor(result2);
            return new UpdateAuthoreResponse { HttpStatusCode = System.Net.HttpStatusCode.OK, Author = result2 };
        }
    }
}
