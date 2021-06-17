using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Persistence
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(DbContextOptions<BaseDataContext> options) : base (options)
        {

        }

        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Executed when run Add-Migration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: identify seeds fake vs system seeds
            modelBuilder.Seed();
        }
    }
}
