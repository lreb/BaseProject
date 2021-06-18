using BaseProjectAPI.Persistence;
using BaseProjectAPI.Persistence.Seeds;
using BaseProjectAPI.Service.Items;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Postgres;
using System.IO;
using System.Reflection;

namespace BaseProject.Test
{
    /// <summary>
    /// Custom Factory startup for integration est
    /// </summary>
    public class BaseProjectApiWebApplicationFactory : WebApplicationFactory<BaseProjectAPI.Startup>
    {
        /// <summary>
        /// Uses https://github.com/sandord/Respawn.Postgres to clean up database after integration test
        /// </summary>
        //private readonly PostgresCheckpoint _checkpointPostgreSQL = new PostgresCheckpoint
        //{
        //    AutoCreateExtensions = true,
        //    SchemasToInclude = new[] {
        //        "public"
        //    }
        //};

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

                services.AddScoped<IItemsService, ItemsService>();
                services.AddMediatR(Assembly.GetExecutingAssembly());

                // Add ApplicationDbContext using an in-memory database for testing. Be sure to create first the DB
                services.AddDbContext<BaseDataContext>(c => {
                    c.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                });

                using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();

                    var checkpoint = new PostgresCheckpoint
                    {
                        SchemasToInclude = new[]
                        {
                            "public"
                        },
                        //DbAdapter = DbAdapter.Postgres
                    };

                    checkpoint.Reset(conn.ToString());
                }


                // Build the service provider.
                var serviceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using var scope = serviceProvider.CreateScope();

                MigrationManager.ApplyMigrations(scope);


                //TODO: RESPAWN doesn't clean the db after testing
                //_checkpointPostgreSQL.Reset(Configuration.GetConnectionString("DefaultConnection"));
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
