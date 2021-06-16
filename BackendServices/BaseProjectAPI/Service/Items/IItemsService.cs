using BaseProjectAPI.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items
{
    public interface IItemsService
    {

        Task<IEnumerable<Item>> GetItemsList();
        Task<Item> GetItemById(int id);
        Task<Item> CreateItem(Item Item);
        Task<int> UpdateItem(Item Item);
        Task<int> DeleteItem(Item Item);
    }
}
