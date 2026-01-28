using Microsoft.OpenApi;

namespace BaseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            
            // Add API Explorer (required for Swashbuckle)
            builder.Services.AddEndpointsApiExplorer();
            
            // Add Swashbuckle Swagger generator
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Base API",
                    Version = "v1",
                    Description = "A sample ASP.NET Core Web API with Swagger documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your.email@example.com"
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger middleware
                app.UseSwagger();
                
                // Enable Swagger UI
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Base API v1");
                    options.RoutePrefix = "swagger"; // Access at /swagger
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
