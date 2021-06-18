using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items
{
    public class ItemsService : IItemsService
    {

        private readonly BaseDataContext _context;

        public ItemsService(BaseDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsList(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return await _context.Items
                .EnabledItems()
                .SpecificYearsAgo(5)
                .ToListAsync(token);

            // just an example how to call Expression Func
            // bool d = false;
            // foreach (var item in r)
            // {
            //    d = ItemFilters.IsEnabledExpression.Compile()(item);
            // }
        }

        public async Task<Item> GetItemById(int id, CancellationToken cancellationToken)
        {
            return await _context.Items.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Item> CreateItem(Item Item)
        {
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Item;
        }

        public async Task<int> UpdateItem(Item Item)
        {
            _context.Items.Update(Item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteItem(Item Item)
        {
            _context.Items.Remove(Item);
            return await _context.SaveChangesAsync();
        }
    }
}
