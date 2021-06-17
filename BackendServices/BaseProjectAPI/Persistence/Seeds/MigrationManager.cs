using BaseProjectAPI.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BaseProjectAPI.Persistence.Seeds
{
    /// <summary>
    /// Execute pending migrations when the applications starts
    /// </summary>
    public static class MigrationManager
    {
        /// <summary>
        /// Execute pending migrations when the applications starts
        /// </summary>
        /// <param name="host"></param>
        /// <returns><see cref="IHost"/></returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                ApplyMigrations(scope);
            }
            return host;
        }

        /// <summary>
        /// Retrieve app context from <see cref="IServiceProvider"/> to apply migrations
        /// </summary>
        /// <param name="scope"></param>
        public static void ApplyMigrations(IServiceScope scope)
        {
            using var appContext = scope.ServiceProvider.GetRequiredService<BaseDataContext>();

            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new BaseException("Cannot apply pending migrations", ex);
            }
        }
    }
}
