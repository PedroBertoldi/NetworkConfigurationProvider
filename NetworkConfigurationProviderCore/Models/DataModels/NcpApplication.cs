using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkConfigurationProviderCore.Models.DataModels
{
    public class NcpApplication
    {
        public NcpApplication()
        {
            Environments = new List<NcpEnvironment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public IList<NcpEnvironment> Environments { get; set; }
    }
}
