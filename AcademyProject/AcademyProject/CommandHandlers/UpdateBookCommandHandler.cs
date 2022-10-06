using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Response;
using AutoMapper;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookInMemoryRepo bookInMemoryRepo, IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
            _authorInMemoryRepo = authorInMemoryRepo;
            _mapper = mapper;
        }

        public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (request._book == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _bookInMemoryRepo.GetById(request._book.Id);
            if (result == null) return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Book" };

            var authorExist = await _authorInMemoryRepo.GetById(request._book.AuthorId);
            if (authorExist == null) return new UpdateBookResponse()
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Author with this ID : {request._book.AuthorId} does not exist" };

            var resultFromMap = _mapper.Map<Book>(request._book);
            resultFromMap.LastUpdated = DateTime.UtcNow;
            await _bookInMemoryRepo.UpdateBook(resultFromMap);
            return new UpdateBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }
    }
}
