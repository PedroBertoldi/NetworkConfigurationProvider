using Dapper;
using Microsoft.Extensions.Configuration;
using NetworkConfigurationProviderCore.Models.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkConfigurationProviderServer.Repository
{
    public class NetworkConfigurationProviderRepository : INetworkConfigurationProviderRepository, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public NetworkConfigurationProviderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public async Task<Dictionary<string, string>> GetKeysForApplicationAsync(string applicationId, string applicationSecret, string environment)
        {
            var data = new Dictionary<string, string>();

            var result = await _connection.QueryAsync(@"
                select data.[Key], data.[Value]
                from NcpData data
                inner join NcpEnvironment env on env.Id = data.EnvironmentId
                inner join NcpApplication app on app.Id = data.ApplicationId
                where app.Name = @applicationId
                and (app.Secret = @applicationSecret or @applicationSecret is null)
                and env.Name = @environment
            ", new { applicationId, applicationSecret, environment });

            if (result.Any())
            {
                foreach ( var item in result)
                {
                    data[item.Key] = item.Value;
                }
            }

            return data;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
