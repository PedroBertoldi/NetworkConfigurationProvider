using Microsoft.Extensions.Configuration;
using NetworkConfigurationProviderCore.Core;
using NetworkConfigurationProviderCore.Models.Settings;
using System;
using System.Net.Http;
using System.Threading;

namespace NetworkConfigurationProviderClient.Core
{
    public class NetworkConfigurationProvider : ConfigurationProvider
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly NcpSettings _settings;
        private Timer _timer;

        public NetworkConfigurationProvider(NcpSettings settings)
        {
            _settings = settings;
            GetReloadToken().RegisterChangeCallback(obj => Load(), null);
        }

        public override void Load()
        {
            try
            {
                if (string.IsNullOrEmpty(_settings.ProviderUrl))
                {
                    throw new ArgumentNullException($"{nameof(NcpSettings.ProviderUrl)} is a required field");
                }

                var raw = NetworkConfigurationProviderHelper.GetData(_httpClient, _settings);
                foreach (var item in raw)
                {
                    Data[item.Key] = item.Value;
                }

                if (_timer == null && _settings.FetchInterval.HasValue)
                {
                    _timer = new Timer((x) => Load(), null, 0, (int)_settings.FetchInterval.Value.TotalMilliseconds);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
