using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.MediatR.Commands;
using AcademyProjectModels.Response;
using AutoMapper;
using MediatR;

namespace AcademyProject.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookInMemoryRepo bookInMemoryRepo, IAuthorInMemoryRepo authorInMemoryRepo, IMapper mapper)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
            _authorInMemoryRepo = authorInMemoryRepo;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _bookInMemoryRepo.GetBookByTitle(request._book.Title);
            if (result != null) return new AddBookResponse()
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This Book already exists" };

            var authorExist = await _authorInMemoryRepo.GetById(request._book.AuthorId);
            if (authorExist == null) return new AddBookResponse()
            { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = $"Author with this ID : {request._book.AuthorId} does not exist" };

            var resultFromMap = _mapper.Map<Book>(request._book);
            resultFromMap.LastUpdated = DateTime.UtcNow;
            await _bookInMemoryRepo.AddBook(resultFromMap);
            return new AddBookResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Book = resultFromMap };
        }
    }
}
