using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetworkConfigurationProviderClient.Core;

namespace NetworkConfigurationProviderClient.Controllers
{
    [Route("[controller]")]
    public class NetworkConfigurationController : ControllerBase
    {
        protected readonly IConfiguration _configuration;

        public NetworkConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Reload")]
        public virtual IActionResult Reload()
        {
            _configuration.ReloadNetworkConfigurationProvider();
            return NoContent();
        }
    }
}
