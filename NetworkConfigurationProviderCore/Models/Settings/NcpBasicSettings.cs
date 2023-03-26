using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkConfigurationProviderCore.Models.Settings
{
    public class NcpBasicSettings
    {
        /// <summary>
        /// Url of the provider server.
        /// </summary>
        public virtual string ProviderUrl { get; set; }

        /// <summary>
        /// Id of the client.
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Secret of the client.
        /// </summary>
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// Environment to retreave key values.
        /// </summary>
        public virtual string Environment { get; set; }
    }
}
