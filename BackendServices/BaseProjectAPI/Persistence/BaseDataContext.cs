using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Persistence
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(DbContextOptions<BaseDataContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Executed when run Add-Migration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // override model configurations
            modelBuilder.FluentAPIConfiguration();

            //TODO: identify seeds fake vs system seeds
            modelBuilder.Seed();
        }

        
    }
}
