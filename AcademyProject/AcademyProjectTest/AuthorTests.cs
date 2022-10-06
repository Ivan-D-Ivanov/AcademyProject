using AcademyProject.AutoMapping;
using AcademyProject.Controllers;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.Request;
using AcademyProjectModels.Response;
using AcademyProjectSL.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AcademyProjectTest
{
    public class AuthorTests
    {
        private IList<Author> _authors = new List<Author>()
        {
             new Author(){ Id = 1, Age = 20, DateOfBirth = DateTime.Now, Name = "Dishsoup", NickName = "SoupDish" },
             new Author(){ Id = 2, Age = 21, DateOfBirth = DateTime.Now, Name = "Dishsoup123", NickName = "SoupDish123" },
             new Author(){ Id = 3, Age = 23, DateOfBirth = DateTime.Now, Name = "Dish321", NickName = "Soup321" }
        };

        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AuthorController>> _authorControllerMockLogger;
        private readonly Mock<IAuthorInMemoryRepo> _authorMockedRepo;
        private readonly Mock<IBookInMemoryRepo> _bookInMemoryRepoMockedRepo;

        public AuthorTests()
        {
            var mockedMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });

            _mapper = mockedMapper.CreateMapper();
            _authorMockedRepo = new Mock<IAuthorInMemoryRepo>();
            _bookInMemoryRepoMockedRepo = new Mock<IBookInMemoryRepo>();
            _authorControllerMockLogger = new Mock<ILogger<AuthorController>>();
        }

        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedResult = 3;

            _authorMockedRepo.Setup(x => x.GetAuthors()).ReturnsAsync(_authors);

            //inject
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //Act
            var currResult = await controller.GetAllAuthors();

            //Assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;
            Assert.NotEmpty(authors);
            Assert.NotNull(authors);
            Assert.Equal(expectedResult, authors.Count());
        }

        [Fact]
        public async Task Author_GetById_Check()
        {
            //setup
            var authorId = 1;
            var expectedAuthor = _authors.First(x => x.Id == 1);

            _authorMockedRepo.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedAuthor);

            //inject
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var currResult = await controller.GetById(authorId);

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public async Task Author_GetById_Check_NotFound()
        {
            //setup
            var authorId = 10;

            _authorMockedRepo.Setup(x => x.GetById(authorId)).ReturnsAsync(_authors.FirstOrDefault(x=> x.Id == authorId));

            //inject
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var currResult = await controller.GetById(authorId);

            //assert
            var notObjectResult = currResult as NotFoundObjectResult;
            Assert.NotNull(notObjectResult);

            var id = (int)notObjectResult.Value;
            Assert.Equal(authorId, id);
        }

        [Fact]
        public async Task AddAuthorOk()
        {
            //setup
            var authorReq = new AddAuthorRequest() { Name = "ivan", Age = 2, DateOfBirth = DateTime.UtcNow, NickName = "asdasdasd" };

            _authorMockedRepo.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                _authors.Add(new Author { Id = 4, Name = authorReq.Name, Age = authorReq.Age, DateOfBirth = authorReq.DateOfBirth, NickName = authorReq.NickName });
            }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == 4));

            //inject 
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var result = await controller.AddAuthor(authorReq);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddAuthorResponse;

            Assert.NotNull(resultValue);
            Assert.Equal(4, resultValue.Author.Id);
        }

        [Fact]
        public async Task Author_AddAuthorThatExists()
        {
            //setup
            var authorReq = new AddAuthorRequest() { Name = "Dishsoup", Age = 20, DateOfBirth = DateTime.UtcNow, NickName = "SoupDish" };

            _authorMockedRepo.Setup(x => x.GetAuthorByName(authorReq.Name)).ReturnsAsync(
                _authors.FirstOrDefault(x => x.Name == authorReq.Name));

            //inject 
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var result = await controller.AddAuthor(authorReq);

            //assert
            var badObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badObjectResult);
            
            var resultValue = badObjectResult.Value as AddAuthorResponse;
            Assert.Equal(resultValue.Author.Name, authorReq.Name);
        }

        [Fact]
        public async Task DeleteAuthorById()
        {
            var authorId = 1;
            var expectedAuthor = _authors.First(x => x.Id == authorId);

            _authorMockedRepo.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedAuthor);
            _authorMockedRepo.Setup(x => x.DeleteAuthor(authorId)).ReturnsAsync(expectedAuthor);

            //inject
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var currResult = await controller.DeleteAuthorById(authorId);

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public async Task DeleteNotFound()
        {
            var authorId = 5;
            var expectedResult = _authors.FirstOrDefault(x => x.Id == authorId);
            _authorMockedRepo.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedResult);

            //inject
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var currResult = await controller.DeleteAuthorById(authorId);

            //assert
            var notFoundResult = currResult as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);

            var authorIdNotFound = (int)notFoundResult.Value;
            Assert.Equal(authorId, authorIdNotFound);
        }

        [Fact]
        public async Task Author_UpdateAuthorOk()
        {
            var authorReq = new UpdateAuthorRequest() { Name = "Dishsoup", Age = 20, DateOfBirth = DateTime.UtcNow, NickName = "Fool" };

            _authorMockedRepo.Setup(x => x.GetAuthorByName(authorReq.Name)).ReturnsAsync(
                _authors.FirstOrDefault(x => x.Name == authorReq.Name));

            _authorMockedRepo.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                _authors.RemoveAt(0);
                _authors.Add(new Author() { Id = 1, Name = authorReq.Name, DateOfBirth = authorReq.DateOfBirth, Age = authorReq.Age, NickName = authorReq.NickName });
            }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == 1));

            //inject 
            var service = new AuthorService(_authorMockedRepo.Object, _mapper, _bookInMemoryRepoMockedRepo.Object);
            var controller = new AuthorController(_authorControllerMockLogger.Object, service);

            //act
            var result = await controller.UpdateAuthor(authorReq);

            //assert
            var badObjectResult = result as OkObjectResult;
            Assert.NotNull(badObjectResult);

            var resultValue = badObjectResult.Value as UpdateAuthoreResponse;
            Assert.Equal(resultValue.Author.Name, authorReq.Name);
        }
    }
}