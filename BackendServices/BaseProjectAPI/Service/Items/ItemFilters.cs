using BaseProjectAPI.Domain.Enums;
using BaseProjectAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaseProjectAPI.Service.Items
{
    public static class ItemFilters
    {
        /// <summary>
        /// Retrieve most recent items
        /// </summary>
        /// <param name="query"><see cref="Item"/></param>
        /// <param name="yearsAgo"><see cref="int"/></param>
        /// <returns><see cref="IQueryable"/></returns>
        public static IQueryable<Item> SpecificYearsAgo(this IQueryable<Item> query, int yearsAgo)
        {
            return query.Where(c => c.CreatedOn > DateTime.Now.AddYears(-yearsAgo));
        }

        /// <summary>
        /// Evaluate Items with specific date
        /// </summary>
        private static Expression<Func<Item, int, bool>> SpecificYearsAgoExpression { get; } = (item, years) => item.CreatedOn > DateTime.Now.AddYears(-years);

        /// <summary>
        /// Retrieves enabled items
        /// </summary>
        /// <param name="items"><see cref="Item"/></param>
        /// <returns><see cref="IQueryable"/></returns>
        public static IQueryable<Item> EnabledItems(this IQueryable<Item> items)
        {
            return items.Where(IsEnabledExpression);
        }

        /// <summary>
        /// Expression to validate if Item is enabled
        /// </summary>
        public static Expression<Func<Item, bool>> IsEnabledExpression { get; } = item => item.IsEnabled;

        public static string GetStockStatus(int quantity)
        {
            StockStatus result;
            if (quantity <= 0)
                result = StockStatus.Empty;
            else if (quantity > 0 && quantity <= 2)
                result = StockStatus.Low;
            else if (quantity > 2 && quantity <= 5)
                result = StockStatus.Medium;
            else
                result = StockStatus.Good;
            
            return result.ToString();
        }
    }
}