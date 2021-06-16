using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;

namespace BaseProjectAPI.Infraestructure.Extensions
{
    public static class HealtChecksExtension
    {
        /// <summary>
        /// Generates full detail from all checks
        /// </summary>
        /// <param name="endpoints"><see cref="IEndpointRouteBuilder"/></param>
        internal static void MapAllDataFromChecks(this IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapHealthChecks("/health/all");


            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

        /// <summary>
        /// Generates summary detail from all checks
        /// </summary>
        /// <param name="endpoints"><see cref="IEndpointRouteBuilder"/></param>
        internal static void MapSummaryDataFromChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health-details",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                status = report.Status.ToString(),
                                monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                }
            );
        }

        /// <summary>
        /// Map checks for data source
        /// </summary>
        /// <param name="endpoints"></param>
        internal static void MapSourceDataChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/datasource", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("DataSource"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

        /// <summary>
        /// Map checks for examples
        /// </summary>
        /// <param name="endpoints"></param>
        internal static void MapExampleChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/example", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("example1") || check.Tags.Contains("example2"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        }
    }
}
