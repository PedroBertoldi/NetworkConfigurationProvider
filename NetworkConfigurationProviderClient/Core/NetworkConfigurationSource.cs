using Microsoft.Extensions.Configuration;
using NetworkConfigurationProviderCore.Models.Settings;

namespace NetworkConfigurationProviderClient.Core
{
    public class NetworkConfigurationSource : IConfigurationSource
    {
        private readonly NcpSettings _settings;

        public NetworkConfigurationSource(NcpSettings settings)
        {
            _settings = settings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new NetworkConfigurationProvider(_settings);
        }
    }
}
