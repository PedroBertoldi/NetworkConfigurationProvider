using Microsoft.Extensions.Configuration;
using NetworkConfigurationProviderCore.Models.Settings;
using System;
using System.Linq;

namespace NetworkConfigurationProviderClient.Core
{
    public static class NetworkConfigurationExtensions
    {
        public static IConfigurationBuilder AddNetworkConfigurationProvider(this IConfigurationBuilder builder,
            Action<NcpSettings> options = null)
        {
            var config = new NcpSettings();
            TryBasicBindings(builder, config);

            if (options != null)
            {
                options.Invoke(config);
            }

            builder.Add(new NetworkConfigurationSource(config));

            return builder;
        }

        public static IConfiguration ReloadNetworkConfigurationProvider(this IConfiguration configuration)
        {
            var root = (IConfigurationRoot)configuration;

            foreach (var provider in root.Providers.Where(x => x.GetType() == typeof(NetworkConfigurationProvider)))
            {
                provider.Load();
            }

            return configuration;
        }

        private static NcpSettings TryBasicBindings(this IConfigurationBuilder builder, NcpSettings settings)
        {
            var data = builder.Build()
                .GetSection(nameof(NcpSettings))
                .GetChildren()
                .ToDictionary(k => k.Key, v => v.Value);

            if (data.Count == 0)
            {
                return settings;
            }

            var properties = typeof(NcpSettings).GetProperties()
                .Where(x => !x.PropertyType.IsClass || x.PropertyType == typeof(string))
                .ToDictionary(k => k.Name, v => v);

            foreach (var key in data.Keys.Where(k => properties.ContainsKey(k)))
            {
                properties[key].SetValue(settings, data[key]);
            }

            return settings;
        }
    }
}
