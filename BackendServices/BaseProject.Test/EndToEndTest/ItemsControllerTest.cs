using BaseProjectAPI.Service.Items.Commands;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace BaseProject.Test.EndToEndTest
{
    [Trait("Category", "E2e")]
    //[TestCaseOrderer("XUnit.Project.Orderers.PriorityOrderer", "XUnit.Project")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ItemsControllerTest : IntegrationTestWrapper
    {
        /// <summary>
        /// inherits from the custom factory
        /// </summary>
        /// <param name="fixture"><see cref="BaseProjectApiWebApplicationFactory"/></param>
        public ItemsControllerTest(BaseProjectApiWebApplicationFactory fixture)
        : base(fixture) { }
        
        [Fact, Priority(2)]
        public async Task Get_Should_Retrieve_Items()
        {
            var response = await _client.GetAsync("/api/Items");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(3)]
        public async Task Get_Should_Retrieve_Item()
        {
            var response = await _client.GetAsync("/api/Items/2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(1)]
        public async Task Get_Should_Create_Item()
        {
            CreateItemCommand body = new CreateItemCommand()
            {
                Name = "NameTest",
                IsEnabled = true,
                Quantity = 2
            };
            string payload = JsonSerializer.Serialize(body);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Items", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
