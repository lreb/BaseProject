using BaseProjectAPI.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using static Bogus.DataSets.Name;

namespace BaseProjectAPI.Persistence.Seeds
{
    public static class ModelBuilderExtensions
    {

        /// <summary>
        /// Set fake data generation with Bogus - https://github.com/bchavez/Bogus
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Faker<Item> items = SeedItems();
            Faker<User> users = SeedUsers();
            modelBuilder.Entity<Item>().HasData(items.Generate(10));
            modelBuilder.Entity<User>().HasData(users.Generate(10));
        }

        /// <summary>
        /// Fake items
        /// </summary>
        /// <returns><see cref="Item"/></returns>
        public static Faker<Item> SeedItems()
        {
            var ids = 1;
            return new Faker<Item>()
                .RuleFor(p => p.Id, s => ids++)
                .RuleFor(p => p.Name, (s) => s.Name.FirstName(Gender.Female))
                .RuleFor(p => p.Description, (s) => s.Name.JobDescriptor())
                .RuleFor(p => p.Quantity, (s) => s.Random.Number(1, 10))
                .RuleFor(p => p.IsEnabled, s => s.Random.Bool())
                .RuleFor(p => p.CreatedOn, s => s.Date.Past(10, DateTime.Now));
        }

        /// <summary>
        /// Fake users
        /// </summary>
        /// <returns><see cref="User"/></returns>
        public static Faker<User> SeedUsers()
        {
            var ids = 1;
            return new Faker<User>()
                .RuleFor(p => p.Id, s => ids++)
                .RuleFor(p => p.FirstName, (s) => s.Name.FirstName(Gender.Male))
                .RuleFor(p => p.LastName, (s) => s.Name.LastName(Gender.Male))
                .RuleFor(p => p.Email, (s, u) => s.Internet.Email(u.FirstName, u.LastName))

                .RuleFor(p => p.IsEnabled, s => s.Random.Bool())
                .RuleFor(p => p.CreatedOn, s => s.Date.Past(10, DateTime.Now));
        }

        /// <summary>
        /// Setup fluent API configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void FluentAPIConfiguration(this ModelBuilder modelBuilder)
        {
            // example: modelBuilder.Entity<Item>().HasIndex(p => p.Name);
            //modelBuilder.Entity<Item>(e => e);

            modelBuilder.Entity<Item>(builder => 
            {
                builder.HasIndex(p => new { p.Name, p.Quantity, p.CreatedOn });
                builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
                builder.Property(s => s.Description).IsRequired().HasMaxLength(300);
                builder.Property(x=>x.CreatedOn).HasDefaultValueSql("TIMEZONE('utc', NOW())").ValueGeneratedOnAdd();
            });
            
        }
    }
}
