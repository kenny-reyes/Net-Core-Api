using Microsoft.Extensions.Configuration;

namespace NetCoreApiScaffolding.Tools.Extensions.Configuration
{
    public static class ConfigurationSectionExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration, string key = "") where T : class, new()
        {
            var configKey = string.IsNullOrEmpty(key) ? typeof(T).Name : key;
            var instance = new T();

            configuration.GetSection(configKey).Bind(instance);

            return instance;
        }
    }
}