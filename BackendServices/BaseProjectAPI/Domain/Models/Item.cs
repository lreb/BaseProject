using BaseProjectAPI.Domain.Models.BaseModels;

namespace BaseProjectAPI.Domain.Models
{
    public class Item : BaseModelLevel1
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
