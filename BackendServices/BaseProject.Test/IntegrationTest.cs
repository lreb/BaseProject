using Microsoft.Extensions.Configuration;
using Respawn.Postgres;
using System.Net.Http;
using Xunit;

namespace BaseProject.Test
{
    public abstract class IntegrationTestWrapper : IClassFixture<BaseProjectApiWebApplicationFactory>
    {
        /// <summary>
        /// Uses https://github.com/sandord/Respawn.Postgres to clean up database after integration test
        /// </summary>
        private readonly PostgresCheckpoint _checkpointPostgreSQL = new PostgresCheckpoint
        {
            AutoCreateExtensions = true,
            SchemasToInclude = new[] {
            "public"
            }
        };

        /// <summary>
        /// Custom startup
        /// </summary>
        protected readonly BaseProjectApiWebApplicationFactory _factory;

        /// <summary>
        /// HTTP client service for web calls
        /// </summary>
        protected readonly HttpClient _client;

        /// <summary>
        /// Initializes all common services
        /// </summary>
        /// <param name="fixture"></param>
        public IntegrationTestWrapper(BaseProjectApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();

            var str = _factory.Configuration.GetConnectionString("DefaultConnection");
            _checkpointPostgreSQL.Reset(str);
        }
    }
}
