using Microsoft.Extensions.Configuration;

namespace DotNetCqrsApi.Api.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration, string key = "")  where T: class, new()
        {
            var configKey = string.IsNullOrEmpty(key) ? typeof(T).Name : key;
            var instance = new T();
                
            configuration.GetSection(configKey).Bind(instance);
            
            return instance;
        }
    }
}