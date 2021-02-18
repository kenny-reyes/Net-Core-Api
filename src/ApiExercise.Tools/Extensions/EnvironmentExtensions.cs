using Microsoft.Extensions.Hosting;

namespace ApiExercise.Tools.Extensions
{
    public static class EnvironmentExtensions
    {
        private static readonly string EnvironmentLocalName = "Local";

        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment(EnvironmentLocalName);
        }
    }
}