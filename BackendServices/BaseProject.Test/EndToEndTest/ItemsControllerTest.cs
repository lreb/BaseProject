using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.EndToEndTest
{
    public class ItemsControllerTest : IntegrationTestWrapper
    {
        /// <summary>
        /// inherits from the custom factory
        /// </summary>
        /// <param name="fixture"><see cref="BaseProjectApiWebApplicationFactory"/></param>
        public ItemsControllerTest(BaseProjectApiWebApplicationFactory fixture)
        : base(fixture) { }

        [Fact]
        public async Task Get_Should_Retrieve_Items()
        {
            var response = await _client.GetAsync("/api/Items");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
