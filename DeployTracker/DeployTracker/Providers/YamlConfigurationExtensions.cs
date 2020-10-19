using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Providers
{
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYaml(
        this IConfigurationBuilder builder, string filePath)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            return builder
                .Add(new YamlConfigurationSource(filePath));
        }
    }
}
