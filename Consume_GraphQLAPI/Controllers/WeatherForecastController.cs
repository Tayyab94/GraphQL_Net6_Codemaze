using Consume_GraphQLAPI.DataClasses;
using Consume_GraphQLAPI.DataClasses.MutationFolder;
using Microsoft.AspNetCore.Mvc;

namespace Consume_GraphQLAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly OwnerConsumer consumer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, OwnerConsumer consumer)
        {
            _logger = logger;
            this.consumer = consumer;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOwnersList()
        {
            var owners = await consumer.GetAllOwners();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetOwnerById(Guid id)
        {
            var result = await consumer.GetOwner(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>CreateOwner([FromBody]OwnerInput owner)
        {
            var createOwner = await consumer.CreateOwner(owner);

            return Ok(createOwner);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id,[FromBody] OwnerInput owner)
        {
            var updateOwner = await consumer.UpdateOwner(id,owner);

            return Ok(updateOwner);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteOwner(Guid id)
        {
            var result = await consumer.DeleteOwner(id);
            return Ok(result);
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}