using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaPublisherController : ControllerBase
    {
        private readonly IKafkaPublisherService<int, Person> _kafkaPublisherService;

        public KafkaPublisherController(IKafkaPublisherService<int, Person> kafkaPublisherService)
        {
            _kafkaPublisherService = kafkaPublisherService;
        }

        [HttpPost(nameof(Publish))]
        public async Task<IActionResult> Publish(int key, Person value)
        {
            await _kafkaPublisherService.PublishTopic(key, value);
            return Ok();
        }
    }
}
