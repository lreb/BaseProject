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
using System.Reflection;

namespace BaseProjectAPI
{
    public class Startup
    {
        /// <summary>
        /// Configurations services
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// Constructor where we inject the configuration settings
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BaseDataContext>(c =>
            {
                c.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers()
                // register FluenValidator
                .AddFluentValidation(r =>
                {
                    r.RegisterValidatorsFromAssemblyContaining<Startup>();
                    // It is possible to use both Fluent Validation and Data Annotation at a time. Let’s only support Fluent Validation for now.
                    //r.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    r.DisableDataAnnotationsValidation = true;
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
                .AddNpgSql(_configuration.GetConnectionString("DefaultConnection"), name: "Postgresql", failureStatus: HealthStatus.Unhealthy, tags: new[] { "DataSource" })
                .AddCheck("AlwaysHealthy", () => HealthCheckResult.Healthy(), tags: new[] { "Example1" })
                .AddCheck("AlwaysHealthyToo", () => HealthCheckResult.Healthy(), tags: new[] { "Example2" });

            services.AddHealthChecksUI()
                .AddInMemoryStorage();
            #endregion

            #region DI - Register the services and repositories
            // register all injections
            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<IUsersService, UsersService>();
            // register all mediatr handlers
            services.AddMediatR(Assembly.GetExecutingAssembly());
            #endregion

            #region Auto Mapper
            services.AddAutoMapper(typeof(Startup));
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseProjectAPI v1"));
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
