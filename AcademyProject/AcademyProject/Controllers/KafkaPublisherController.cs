using AcademyProjectCaches.CacheInMemoryCollection;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaPublisherController : ControllerBase
    {
        private readonly IKafkaPublisherService<int, Book> _kafkaPublisherService;
        private readonly GenericCollection<Book> _books;

        public KafkaPublisherController(IKafkaPublisherService<int, Book> kafkaPublisherService, GenericCollection<Book> books)
        {
            _kafkaPublisherService = kafkaPublisherService;
            _books = books;
        }

        [HttpPost(nameof(Publish))]
        public async Task<IActionResult> Publish(int key, Book value)
        {
            await _kafkaPublisherService.PublishTopic(key, value);
            return Ok();
        }

        [HttpGet(nameof(GetAllFromGenericCollection))]
        public async Task<IActionResult> GetAllFromGenericCollection()
        {
            var result = await _books.GetAllItems();
            return Ok(result);
        }
    }
}
