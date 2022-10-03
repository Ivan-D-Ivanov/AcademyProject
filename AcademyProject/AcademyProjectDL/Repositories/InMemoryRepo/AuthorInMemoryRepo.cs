using AcademyProjectModels;

namespace AcademyProjectDL.Repositories.InMemoryRepo
{
    public class AuthorInMemoryRepo : IAuthorInMemoryRepo
    {
        private static List<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Jorkata",
                Age = 29,
                DateOfBirth = DateTime.UtcNow,
                NickName = "Jikata"
            },
            new Author()
            {
                Id = 2,
                Name = "Gergana",
                Age = 29,
                DateOfBirth = DateTime.UtcNow,
                NickName = "Gerga"
            },
            new Author()
            {
                Id = 3,
                Name = "Vasko",
                Age = 29,
                DateOfBirth = DateTime.UtcNow,
                NickName = "vSto"
            },
            new Author()
            {
                Id = 4,
                Name = "Filip",
                Age = 29,
                DateOfBirth = DateTime.UtcNow,
                NickName = "Filipo Idzagi"
            },
        };


        public Guid Id { get; set; }

        public AuthorInMemoryRepo()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<Author> GetAuthors => _authors;

        public Author? GetById(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }

        public Author? AddAuthor(Author author)
        {
            _authors.Add(author);
            return author;
        }

        public Author? UpdateAuthor(Author author)
        {
            var existingUser = _authors.FirstOrDefault(x => x.Id == author.Id);
            if (existingUser == null) return null;

            _authors.Remove(existingUser);
            _authors.Add(author);

            return author;
        }

        public Author? DeleteAuthor(int authorId)
        {
            var authorToRemove = _authors.FirstOrDefault(x => x.Id == authorId);
            if (authorToRemove == null) return null;
            _authors.Remove(authorToRemove);

            return authorToRemove;
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}
