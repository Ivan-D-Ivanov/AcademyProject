using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorInMemoryRepo _authorInMemoryRepo;

        public AuthorService(IAuthorInMemoryRepo authorInMemoryRepo)
        {
            _authorInMemoryRepo = authorInMemoryRepo;
        }

        public IEnumerable<Author> GetAuthors => _authorInMemoryRepo.GetAuthors;

        public Author? GetById(int id)
        {
            return _authorInMemoryRepo.GetById(id);
        }

        public Author? AddAuthor(Author author)
        {
            _authorInMemoryRepo.AddAuthor(author);
            return author;
        }

        public Author? UpdateAuthor(Author author)
        {
            return _authorInMemoryRepo.UpdateAuthor(author);
        }

        public Author? DeleteAuthor(int authorId)
        {
            return _authorInMemoryRepo.DeleteAuthor(authorId);
        }

        public Guid GetId()
        {
            return _authorInMemoryRepo.GetId();
        }
    }
}
