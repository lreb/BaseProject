using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace BaseProject.Test
{
    /// <summary>
    /// Custom Factory startup for integration est
    /// </summary>
    public class BaseProjectApiWebApplicationFactory : WebApplicationFactory<BaseProjectAPI.Startup>
    {
        public IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Override host configurations
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                // uses a integration test database; NOTE create a new database before run tests
                Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("integrationsettings.json")
                    .Build();

                config.AddConfiguration(Configuration);
            });

            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigStub>();
            });
        }
    }

    public class WeatherForecastConfigStub : IWeatherForecastConfigService
    {
        public int NumberOfDays() => 7;
    }

    public interface IWeatherForecastConfigService
    {
        public int NumberOfDays();
    }
}
