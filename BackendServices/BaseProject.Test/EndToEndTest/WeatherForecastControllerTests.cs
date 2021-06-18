using BaseProjectAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.EndToEndTest
{
    [Trait("Category", "E2e")]
    /// <summary>
    /// test with basic factory
    /// </summary>
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<BaseProjectAPI.Startup>>
    {
        public HttpClient _client { get; }

        public WeatherForecastControllerTests(WebApplicationFactory<BaseProjectAPI.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        // TODO: categorize kind of tests

        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var response = await _client.GetAsync("/WeatherForecast");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());
            forecast.Should().HaveCount(5);
        }
    }


    [Trait("Category", "E2e")]
    /// <summary>
    /// test with custom factory
    /// </summary>
    public class CustomWeatherForecastControllerTests : IntegrationTestWrapper
    {
        /// <summary>
        /// inherits from the custom factory
        /// </summary>
        /// <param name="fixture"><see cref="BaseProjectApiWebApplicationFactory"/></param>
        public CustomWeatherForecastControllerTests(BaseProjectApiWebApplicationFactory fixture)
        : base(fixture) { }

        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var response = await _client.GetAsync("/WeatherForecast");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());
            forecast.Should().HaveCount(5);
        }
    }
}
