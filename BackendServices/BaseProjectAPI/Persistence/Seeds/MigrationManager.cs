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
        /// <returns></returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<BaseDataContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw new BaseException("Cannot apply pending migrations",ex);
                    }
                }
            }
            return host;
        }
    }
}
