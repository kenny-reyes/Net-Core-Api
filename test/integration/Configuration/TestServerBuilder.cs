using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunctionalTests.Configuration
{
    public class TestServerBuilder
    {
        private const string AppSettingsFileName = "appsettings.json";
        private Type _startupType;

        private TestServerBuilder()
        {
        }

        public static TestServerBuilder Create()
        {
            return new TestServerBuilder();
        }

        public TestServerBuilder WithStartup<TStartup>() where TStartup : class
        {
            _startupType = typeof(TStartup);
            return this;
        }

        public TestServer Build()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(AppSettingsFileName)
                    .AddEnvironmentVariables()
                    .Build())
                .ConfigureLogging(builder => builder.AddConsole())
                .UseStartup(_startupType);

            return new TestServer(webHostBuilder);
        }
    }
}