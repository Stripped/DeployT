using Microsoft.Extensions.Configuration;
using System;

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
