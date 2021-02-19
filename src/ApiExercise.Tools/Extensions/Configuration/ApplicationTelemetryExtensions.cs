using ApiExercise.Tools.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace ApiExercise.Tools.Extensions.Configuration
{
	public static class ApplicationTelemetryExtensions
    {
        public static void ConfigureApplicationInsights(this IConfiguration configuration)
        {
            var customConfigSection = configuration.GetSection<ApplicationInsights>();
            var useApplicationInsightsTelemetries =
                !string.IsNullOrEmpty(customConfigSection?.InstrumentationKey) && !customConfigSection.DisableApiTelemetries;

            if (useApplicationInsightsTelemetries)
            {
                TelemetryConfiguration.Active.InstrumentationKey = customConfigSection.InstrumentationKey;    
            }
            else
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;
            }
        }
    }
}
