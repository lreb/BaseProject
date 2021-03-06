using BaseProjectAPI.Domain.Helpers;
using BaseProjectAPI.Infraestructure.Extensions;
using BaseProjectAPI.Persistence;
using BaseProjectAPI.Service.Items;
using BaseProjectAPI.Service.Users;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Text;

namespace BaseProjectAPI
{
    public class Startup
    {
        /// <summary>
        /// Configurations services
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// The ConnectionStrings Options snapshot
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Constructor where we inject the configuration settings
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Load configuration settings - Options Pattern/>
        /// </summary>
        /// <param name="services">app services <see cref="IServiceCollection"/></param>
        private void ConfigureConfigSettings(IServiceCollection services)
        {
            services
               .AddOptions()
               .Configure<JwtOptions>(_configuration.GetSection($"{nameof(AppSettings)}:{nameof(JwtOptions)}"));

            services
               .AddOptions()
               .Configure<JwtOptions>(_configuration.GetSection(nameof(ConnectionStrings)));

            var serviceProvider = services.BuildServiceProvider();

            _connectionString = GetApplicationConnectionString(); //Environment.GetEnvironmentVariable("BaseProjectDefaultConnectionString");
        }

        private string GetApplicationConnectionString() 
        {
            string dbServer = Environment.GetEnvironmentVariable("BaseProjectDbServer");
            string dbServerPort = Environment.GetEnvironmentVariable("BaseProjectDbServerPort");
            string dbServerUser = Environment.GetEnvironmentVariable("BaseProjectDbServerUser");
            string dbServerPassword = Environment.GetEnvironmentVariable("BaseProjectDbServerPassword");
            string db = Environment.GetEnvironmentVariable("BaseProjectDb");

            //if (String.IsNullOrEmpty(dbServer))
            //    _logger.LogError($"didn't catch '{ nameof(dbServer)}' environment variable");

            //if (String.IsNullOrEmpty(dbServerPort))
            //    _logger.LogError($"didn't catch '{ nameof(dbServerPort)}' environment variable");

            //if (String.IsNullOrEmpty(dbServerUser))
            //    _logger.LogError($"didn't catch '{ nameof(dbServerUser)}' environment variable");

            //if (String.IsNullOrEmpty(dbServerPassword))
            //    _logger.LogError($"didn't catch '{ nameof(dbServerPassword)}' environment variable");

            //if (String.IsNullOrEmpty(db))
            //    _logger.LogError($"didn't catch '{ nameof(db)}' environment variable");

            StringBuilder connectionString = new StringBuilder($"Server={dbServer};port={dbServerPort};user id={dbServerUser};password={dbServerPassword};database={db};pooling=true");

            return connectionString.ToString();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // register FluenValidator
                .AddFluentValidation(r =>
                {
                    r.RegisterValidatorsFromAssemblyContaining<Startup>();
                    // It is possible to use both Fluent Validation and Data Annotation at a time. Let?s only support Fluent Validation for now.
                    r.DisableDataAnnotationsValidation = true;
                });

            ConfigureConfigSettings(services);

            services.AddDbContext<BaseDataContext>(c =>
            {
                c.UseNpgsql(_connectionString);
            });

            #region Swagger service
            // enable swagger service
            services.ConfigureSwaggerExtension(_configuration);
            #endregion

            #region Cors service: enable policy cors service
            services.ConfigureCors(_configuration);
            #endregion

            #region Health check register and enable UI
            services.AddHealthChecks()
                .AddNpgSql(_connectionString, name: "Postgresql", failureStatus: HealthStatus.Unhealthy, tags: new[] { "DataSource" })
                .AddCheck("AlwaysHealthy", () => HealthCheckResult.Healthy(), tags: new[] { "Example1" })
                .AddCheck("AlwaysHealthyToo", () => HealthCheckResult.Healthy(), tags: new[] { "Example2" });

            services.AddHealthChecksUI()
                .AddInMemoryStorage();
            #endregion

            #region DI - Register the services and repositories
            // register all injections
            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUsersSecurityService, UsersSecurityService>();
            
            // register all mediatr handlers
            services.AddMediatR(Assembly.GetExecutingAssembly());
            #endregion

            #region Auto Mapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Enable JWT service
            services.ConfigureJwtService(_configuration);
            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <param name="env"><see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsExtension.AllowSpecificOrigins);

            #region Environment pipeline
            if (env.IsLocal())
            {
                app.UseDeveloperExceptionPage();
                app.EnableSwaggerPipeline(_configuration);
            }
            else if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.EnableSwaggerPipeline(_configuration);
            }
            else if (env.IsStaging())
            {
                app.EnableSwaggerPipeline(_configuration);
            }
            else
            {
                app.EnableSwaggerPipeline(_configuration);
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                #region Map health check endpoints and enable UI
                endpoints.MapAllDataFromChecks();
                endpoints.MapSummaryDataFromChecks();
                endpoints.MapSourceDataChecks();
                endpoints.MapExampleChecks();

                // open Health check panel - <YOUR HOST>/healthchecks-ui#/healthchecks
                endpoints.MapHealthChecksUI();
                #endregion
            });
        }


    }
}
