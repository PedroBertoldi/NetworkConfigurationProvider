using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkConfigurationProviderCore.Models.Settings
{
    public class NcpSettings : NcpBasicSettings
    {
        /// <summary>
        /// Property to set an timer to reload all config keys, set to null if not in use.
        /// </summary>
        public virtual TimeSpan? FetchInterval { get; set; }
    }
}
