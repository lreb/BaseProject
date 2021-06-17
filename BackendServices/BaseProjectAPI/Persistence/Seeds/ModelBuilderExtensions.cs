using BaseProjectAPI.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;

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
            modelBuilder.Entity<Item>().HasData(items.Generate(10));
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
                .RuleFor(p => p.Name, (s, u) => s.Name.FindName(u.Name))
                .RuleFor(p => p.Quantity, (s) => s.Random.Number(1, 10))

                .RuleFor(p => p.IsEnabled, s => s.Random.Bool())
                .RuleFor(p => p.CreatedOn, s => s.Date.Past(10, DateTime.Now))
                .RuleFor(p => p.DisabledOn, s => s.Date.Past(10, DateTime.Now));
        }
    }
}
