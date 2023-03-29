using Microsoft.AspNetCore.Http;
using NetworkConfigurationProviderCore.Models.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NetworkConfigurationProviderCore.Core
{
    public static class NetworkConfigurationProviderHelper
    {
        public static readonly string ClientIdHeader = "ncp-client-id";
        public static readonly string ClientSecretHeader = "ncp-client-secret";

        public static Dictionary<string, string> GetData(HttpClient httpClient, NcpSettings _settings)
        {
            Dictionary<string, string> data = default;

            if (!string.IsNullOrEmpty(_settings.ClientId) && !httpClient.DefaultRequestHeaders.Contains(ClientIdHeader))
            {
                httpClient.DefaultRequestHeaders.Add(ClientIdHeader, _settings.ClientId);
            }

            if (!string.IsNullOrEmpty(_settings.ClientSecret) && !httpClient.DefaultRequestHeaders.Contains(ClientSecretHeader))
            {
                httpClient.DefaultRequestHeaders.Add(ClientSecretHeader, _settings.ClientSecret);
            }

            var url = _settings.ProviderUrl;
            if (!string.IsNullOrEmpty(_settings.Environment))
            {
                url += $"?{nameof(NcpBasicSettings.Environment)}={_settings.Environment}";
            }

            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            }
            else
            {
                throw new Exception($"Fail to contact provider: {content}");
            }

            return data;
        }

        public static (string client, string secret) GetClientAndSecret(this HttpContext context)
        {
            var client = context.Request.Headers[ClientIdHeader];
            var secret = context.Request.Headers[ClientSecretHeader];

            return (client, secret);
        }
    }
}
