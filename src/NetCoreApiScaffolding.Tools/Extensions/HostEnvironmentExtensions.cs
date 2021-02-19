using Microsoft.Extensions.Hosting;

namespace NetCoreApiScaffolding.Tools.Extensions
{
    public static class HostEnvironmentExtensions
    {
        private const string EnvironmentLocalName = "Local";

        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment(EnvironmentLocalName);
        }
    }
}