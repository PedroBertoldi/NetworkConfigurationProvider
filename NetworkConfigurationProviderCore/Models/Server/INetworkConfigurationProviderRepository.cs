using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkConfigurationProviderCore.Models.Server
{
    public interface INetworkConfigurationProviderRepository
    {
        Task<Dictionary<string, string>> GetKeysForApplicationAsync(string applicationId, string applicationSecret, string environment);
    }
}
