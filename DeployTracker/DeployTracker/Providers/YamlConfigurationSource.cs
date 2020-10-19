using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Providers
{
    public class YamlConfigurationSource : FileConfigurationSource
    {
        public YamlConfigurationSource(string fileName)
        {
            Path = fileName;
            ReloadOnChange = true;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            this.EnsureDefaults(builder);
            return new YamlConfigurationProvider(this);
        }
    }
}
