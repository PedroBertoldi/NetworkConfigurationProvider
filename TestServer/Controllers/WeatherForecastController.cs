using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetworkConfigurationProviderCore.Core;
using NetworkConfigurationProviderCore.Models.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INetworkConfigurationProviderRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, INetworkConfigurationProviderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetKeys")]
        public async Task<Dictionary<string, string>> GetKeys([FromQuery] string Environment)
        {
            var data = HttpContext.GetClientAndSecret();
            if (string.IsNullOrEmpty(Environment) || string.IsNullOrEmpty(data.client))
            {
                return new Dictionary<string, string>();
            }

            return await _repository.GetKeysForApplicationAsync(data.client, data.secret, Environment);
        }
    }
}
