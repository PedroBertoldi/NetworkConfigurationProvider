using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkConfigurationProviderCore.Models.DataModels
{
    public class NcpData
    {
        public int Id { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual int EnviromentId { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
        public virtual bool ShareWithFront { get; set; }
        public NcpApplication Application { get; set; }
        public NcpEnvironment Environment { get; set; }
    }
}
