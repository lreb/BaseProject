using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BaseProjectAPI.Infraestructure.Extensions
{
    /// <summary>
    /// Swagger extension
    /// </summary>
    public static class SwaggerExtension
    {
        #region Swagger Configuration
        /// <summary>
        /// Method to configure the Swagger Services within the Application services interface
        /// </summary>
        /// <param name="services">The Service Collection <see cref="IServiceCollection"/></param>
        /// <param name="config">The Service Collection <see cref="IConfiguration"/></param>
        public static void ConfigureSwaggerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = config["SwaggerConfiguration:Title"],
                    Version = config["SwaggerConfiguration:Version"],
                    Description = config["SwaggerConfiguration:Description"]
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);

                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }
        #endregion

        /// <summary>
        /// Enable Swagger pipeline
        /// </summary>
        /// <param name="app">application configuration <see cref="IApplicationBuilder"/></param>
        /// <param name="config">application settings <see cref="IConfiguration"/></param>
        public static void EnableSwaggerPipeline(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(
                    config["SwaggerConfiguration:SwaggerJSONEndpoints"],
                    $"{config["SwaggerConfiguration:Title"]} {config["SwaggerConfiguration:Version"]}");
                // To serve the Swagger UI at the apps root
                //option.RoutePrefix = string.Empty;
            });
        }
    }
}
