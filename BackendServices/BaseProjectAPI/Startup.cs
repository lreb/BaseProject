using BaseProjectAPI.Infraestructure.Extensions;
using BaseProjectAPI.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddDbContext<BaseDataContext>(c => {
                c.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            #region Swagger service
            // enable swagger service
            services.ConfigureSwaggerExtension(_configuration);
            #endregion

            #region Cors service: enable policy cors service
            services.ConfigureCors(_configuration);
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
