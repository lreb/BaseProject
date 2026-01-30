using Asp.Versioning;
using BaseAPI.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BaseAPI.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class VersionController : ControllerBase
{
    private readonly ApiSettings _apiSettings;

    public VersionController(IOptions<ApiSettings> apiSettings)
    {
        _apiSettings = apiSettings.Value;
    }

    /// <summary>
    /// Get API version information
    /// </summary>
    /// <returns>Version information</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyVersion = assembly.GetName().Version;
        var buildDate = GetBuildDate(assembly);

        var versionInfo = new
        {
            ApiVersion = _apiSettings.ApiVersion,
            ApiName = _apiSettings.ApiName,
            AssemblyVersion = assemblyVersion?.ToString() ?? "N/A",
            BuildDate = buildDate,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
            FrameworkVersion = Environment.Version.ToString()
        };

        return Ok(versionInfo);
    }

    private static DateTime? GetBuildDate(Assembly assembly)
    {
        var attribute = assembly.GetCustomAttribute<BuildDateAttribute>();
        return attribute?.DateTime;
    }
}

/// <summary>
/// Attribute to store build date in assembly
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class BuildDateAttribute : Attribute
{
    public DateTime DateTime { get; }

    public BuildDateAttribute(string date)
    {
        DateTime = DateTime.Parse(date);
    }
}
