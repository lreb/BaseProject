using BaseAPI.API.Middleware;
using BaseAPI.API.Configuration;
using BaseAPI.Application;
using BaseAPI.Infrastructure;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;

namespace BaseAPI;

public class Program
{
    public static void Main(string[] args)
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            // Add custom configuration files
            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

            // Configure ApiSettings from appsettings.json
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

            // Add Serilog
            builder.Host.UseSerilog();

            // Add layers
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            // Add controllers
            builder.Services.AddControllers();

            // Add API versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Add Health Checks
            builder.Services.AddHealthChecks()
                .AddNpgSql(
                    builder.Configuration.GetConnectionString("DefaultConnection") ?? "",
                    name: "database",
                    tags: new[] { "db", "postgresql", "npgsql" })
                .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

            // Add API Explorer
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger
            builder.Services.AddSwaggerGen();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Get API version for logging
            var appApiSettings = app.Services.GetRequiredService<IOptions<ApiSettings>>().Value;
            Log.Information("API Version: {Version}", appApiSettings.ApiVersion);

            // Configure the HTTP request pipeline
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Add version to response headers
            app.Use(async (context, next) =>
            {
                var settings = context.RequestServices.GetRequiredService<IOptions<ApiSettings>>().Value;
                context.Response.Headers.Append("X-API-Version", settings.ApiVersion);
                await next();
            });

            //if (app.Environment.IsDevelopment)
            //{
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appApiSettings.ApiName} v{appApiSettings.ApiVersion}");
                    options.RoutePrefix = "swagger";
                    options.DocumentTitle = $"{appApiSettings.ApiName} - v{appApiSettings.ApiVersion}";
                    options.DisplayRequestDuration();
                    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    options.DefaultModelsExpandDepth(-1);
                    
                    // Add custom CSS to display version prominently
                    options.InjectStylesheet("/swagger-ui/custom.css");
                    
                    // Add custom header with version
                    options.HeadContent = $@"
                        <style>
                            .swagger-ui .info .title small.version-stamp {{
                                background-color: #89bf04;
                                color: #fff;
                                padding: 5px 10px;
                                border-radius: 3px;
                                font-size: 16px;
                                font-weight: bold;
                                margin-left: 10px;
                            }}
                            .swagger-ui .info hgroup.main::after {{
                                content: 'v{appApiSettings.ApiVersion}';
                                background-color: #89bf04;
                                color: #fff;
                                padding: 8px 15px;
                                border-radius: 5px;
                                font-size: 18px;
                                font-weight: bold;
                                margin-left: 15px;
                                display: inline-block;
                            }}
                        </style>";
                });
            //}

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            // Health checks endpoints
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });

            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            app.MapControllers();

            Log.Information("?? Application started successfully - Version: v{Version} ??", appApiSettings.ApiVersion);
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
