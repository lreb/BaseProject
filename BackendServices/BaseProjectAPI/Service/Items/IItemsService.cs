using BaseProjectAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items
{
    public interface IItemsService
    {

        Task<IEnumerable<Item>> GetItemsList(CancellationToken cancellationToken);
        Task<Item> GetItemById(int id, CancellationToken cancellationToken);
        Task<Item> CreateItem(Item Item);
        Task<int> UpdateItem(Item Item);
        Task<int> DeleteItem(Item Item);
    }
}
