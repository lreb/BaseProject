using BaseProjectAPI.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BaseProjectAPI.Service.Items
{
    public static class ItemFilters
    {
        public static IQueryable<Item> SpecificYearsAgo(this IQueryable<Item> query, int yearsAgo)
        {
            return query.Where(c => c.CreatedOn > DateTime.Now.AddYears(-yearsAgo));
        }

        private static Expression<Func<Item, int, bool>> SpecificYearsAgoExpression { get; } = (item, years) => item.CreatedOn > DateTime.Now.AddYears(-years);

        public static IQueryable<Item> EnabledItems(this IQueryable<Item> items)
        {
            return items.Where(IsEnabledExpression);
        }

        /// <summary>
        /// Expression to validate if Item is enabled
        /// </summary>
        public static Expression<Func<Item, bool>> IsEnabledExpression { get; } = item => item.IsEnabled;


    }
}
